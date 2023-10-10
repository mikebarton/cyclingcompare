FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["bikecompare.listing.handlers/bikecompare.listing.handlers.csproj", "bikecompare.listing.handlers/"]

RUN dotnet restore "bikecompare.listing.handlers/bikecompare.listing.handlers.csproj"
COPY . .
WORKDIR "/src/bikecompare.listing.handlers"
RUN dotnet build "bikecompare.listing.handlers.csproj" -c Development -o /app

FROM build AS publish
RUN dotnet publish "bikecompare.listing.handlers.csproj" -c Development -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
EXPOSE 8080
ENTRYPOINT ["dotnet", "bikecompare.listing.handlers.dll"]