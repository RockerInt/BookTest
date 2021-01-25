#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Clients/WebApp/WebApp.csproj", "Clients/WebApp/"]
COPY ["Libraries/Utilities/Utilities.csproj", "Libraries/Utilities/"]
COPY ["Libraries/Models/Models.csproj", "Libraries/Models/"]
RUN dotnet restore "Clients/WebApp/WebApp.csproj"
COPY . .
WORKDIR "/src/Clients/WebApp"
RUN dotnet build "WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApp.dll"]
