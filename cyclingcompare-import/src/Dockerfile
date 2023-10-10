FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY ["bikecompare.import.messagehandler/bikecompare.import.messagehandler.csproj", "bikecompare.import.messagehandler/"]
COPY ["bikecompare.import.commissionfactory/bikecompare.import.commissionfactory.csproj", "bikecompare.import.commissionfactory/"]
COPY ["bikecompare.import.messages/bikecompare.import.messages.csproj", "bikecompare.import.messages/"]
COPY ["bikecompare.messages/bikecompare.messages.csproj", "bikecompare.messages/"]
COPY ["./libs", "libs"]

RUN dotnet restore "bikecompare.import.messagehandler/bikecompare.import.messagehandler.csproj"
COPY . .
WORKDIR "/src/bikecompare.import.messagehandler"
RUN dotnet build "bikecompare.import.messagehandler.csproj" -c Development -o /app

FROM build AS publish
RUN dotnet publish "bikecompare.import.messagehandler.csproj" -c Development -o /app

FROM base AS final

RUN apt-get update
RUN apt-get install -y apt-utils
RUN apt-get install -y libgdiplus
RUN apt-get install -y libc6-dev 
RUN ln -s /usr/lib/libgdiplus.so/usr/lib/gdiplus.dll
WORKDIR /app
COPY --from=publish /app .
EXPOSE 8080
ENTRYPOINT ["dotnet", "bikecompare.import.messagehandler.dll"]