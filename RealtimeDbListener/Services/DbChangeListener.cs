using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.SignalR;
using RealtimeDbListener.Hubs;

namespace RealtimeDbListener.Services
{
    public class DbChangeListener
    {
        private readonly string _connectionString;
        private readonly string _sqlQuery;
        private readonly IHubContext<DbChangeHub> _hubContext;

        public DbChangeListener(IConfiguration config, IHubContext<DbChangeHub> hubContext)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")!;
            _sqlQuery = config.GetSection("SqlDependency:Query").Value!;
            _hubContext = hubContext;
        }

        public void StartListening()
        {
            SqlDependency.Start(_connectionString);
            Listen();
        }

        private void Listen()
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(_sqlQuery, connection);

            var dependency = new SqlDependency(command);
            dependency.OnChange += async (s, e) =>
            {
                Listen(); // yeniden abone ol

                await _hubContext.Clients.All.SendAsync("dbChanged", new
                {
                    Info = e.Info.ToString(),
                    Type = e.Type.ToString(),
                    Source = e.Source.ToString(),
                    Time = DateTime.Now
                });

                Console.WriteLine($"📡 DB Change Detected: {e.Info} at {DateTime.Now}");
            };

            connection.Open();
            command.ExecuteReader();
        }
    }
}