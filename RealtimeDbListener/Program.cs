using RealtimeDbListener.Hubs;
using RealtimeDbListener.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddSingleton<DbChangeListener>();

var app = builder.Build();

app.MapGet("/", () => "✅ Realtime DB Listener API running...");
app.MapHub<DbChangeHub>("/hubs/dbChange");

using (var scope = app.Services.CreateScope())
{
    var listener = scope.ServiceProvider.GetRequiredService<DbChangeListener>();
    listener.StartListening();
}

app.Run();