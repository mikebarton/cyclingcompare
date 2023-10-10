FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY ["bikecompare.imaging.handlers/bikecompare.imaging.handlers.csproj", "bikecompare.imaging.handlers/"]
COPY ["bikecompare.imaging.messages/bikecompare.imaging.messages.csproj", "bikecompare.imaging.messages/"]
COPY ["./libs", "libs"]

RUN dotnet restore "bikecompare.imaging.handlers/bikecompare.imaging.handlers.csproj"
COPY . .
WORKDIR "/src/bikecompare.imaging.handlers"
RUN dotnet build "bikecompare.imaging.handlers.csproj" -c Development -o /app

FROM build AS publish
RUN dotnet publish "bikecompare.imaging.handlers.csproj" -c Development -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
EXPOSE 8080
ENTRYPOINT ["dotnet", "bikecompare.imaging.handlers.dll"]