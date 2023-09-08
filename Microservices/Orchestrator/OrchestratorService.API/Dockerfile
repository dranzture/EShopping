FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["OrchestratorService.API/OrchestratorService.API.csproj", "OrchestratorService.API/"]
RUN dotnet restore "OrchestratorService.API/OrchestratorService.API.csproj"
COPY . .
WORKDIR "/src/OrchestratorService.API"
RUN dotnet build "OrchestratorService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrchestratorService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrchestratorService.API.dll"]
