FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CheckoutService.API/CheckoutService.API.csproj", "CheckoutService.API/"]
RUN dotnet restore "CheckoutService.API/CheckoutService.API.csproj"
COPY . .
WORKDIR "/src/CheckoutService.API"
RUN dotnet build "CheckoutService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CheckoutService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CheckoutService.API.dll"]
