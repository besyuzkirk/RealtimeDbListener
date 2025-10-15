FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["RealtimeDbListener/RealtimeDbListener.csproj", "RealtimeDbListener/"]
RUN dotnet restore "RealtimeDbListener/RealtimeDbListener.csproj"
COPY . .
WORKDIR "/src/RealtimeDbListener"
RUN dotnet build "RealtimeDbListener.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RealtimeDbListener.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RealtimeDbListener.dll"]
