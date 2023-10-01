FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ShippingService.API/ShippingService.API.csproj", "ShippingService.API/"]
RUN dotnet restore "ShippingService.API/ShippingService.API.csproj"
COPY . .
WORKDIR "/src/ShippingService.API"
RUN dotnet build "ShippingService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShippingService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShippingService.API.dll"]
