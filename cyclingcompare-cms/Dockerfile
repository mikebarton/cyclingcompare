FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["bikecompare.cms.media.google/bikecompare.cms.media.google.csproj", "bikecompare.cms.media.google/"]
COPY ["bikecompare.cms.web/bikecompare.cms.web.csproj", "bikecompare.cms.web/"]

RUN dotnet restore "bikecompare.cms.media.google/bikecompare.cms.media.google.csproj"
RUN dotnet restore "bikecompare.cms.web/bikecompare.cms.web.csproj"
COPY . .
WORKDIR "/src/bikecompare.cms.web"
RUN dotnet build "bikecompare.cms.web.csproj" -c Development -o /app

FROM build AS publish
RUN dotnet publish "bikecompare.cms.web.csproj" -c Development -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
EXPOSE 8080
ENTRYPOINT ["dotnet", "bikecompare.cms.web.dll"]