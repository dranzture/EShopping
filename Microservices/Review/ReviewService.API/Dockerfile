FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ReviewService.API/ReviewService.API.csproj", "ReviewService.API/"]
RUN dotnet restore "ReviewService.API/ReviewService.API.csproj"
COPY . .
WORKDIR "/src/ReviewService.API"
RUN dotnet build "ReviewService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReviewService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReviewService.API.dll"]
