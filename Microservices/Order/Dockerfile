﻿# Learn about building .NET container images:
# https://github.com/dotnet/dotnet-docker/blob/main/samples/README.md
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY . ./
RUN dotnet restore 

# copy everything else and build app
COPY . ./
RUN dotnet publish -c Release -o out


# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "OrderService.API.dll"]