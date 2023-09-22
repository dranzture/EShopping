FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ShoppingCartService.API/ShoppingCartService.API.csproj", "ShoppingCartService.API/"]
RUN dotnet restore "ShoppingCartService.API/ShoppingCartService.API.csproj"
COPY . .
WORKDIR "/src/ShoppingCartService.API"
RUN dotnet build "ShoppingCartService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShoppingCartService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShoppingCartService.API.dll"]
