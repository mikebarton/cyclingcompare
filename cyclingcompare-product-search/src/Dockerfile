FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["product-search.api/product-search.api.csproj", "product-search.api/"]

RUN dotnet restore "product-search.api/product-search.api.csproj"
COPY . .
WORKDIR "/src/product-search.api"
RUN dotnet build "product-search.api.csproj" -c Development -o /app

FROM build AS publish
RUN dotnet publish "product-search.api.csproj" -c Development -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
EXPOSE 8080
ENTRYPOINT ["dotnet", "product-search.api.dll"]