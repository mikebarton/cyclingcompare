FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY ["buildtester/buildtester.csproj", "buildtester/"]

COPY . .
WORKDIR "/src/buildtester"
RUN dotnet build "buildtester.csproj" -c Development -o /app

FROM build AS publish
RUN dotnet publish "buildtester.csproj" -c Development -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
EXPOSE 8080
ENTRYPOINT ["dotnet", "buildtester.dll"]