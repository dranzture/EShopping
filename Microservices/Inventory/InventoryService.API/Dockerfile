FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["InventoryService.API/InventoryService.API.csproj", "InventoryService.API/"]
RUN dotnet restore "InventoryService.API/InventoryService.API.csproj"
COPY . .
WORKDIR "/src/InventoryService.API"
RUN dotnet build "InventoryService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InventoryService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InventoryService.API.dll"]
