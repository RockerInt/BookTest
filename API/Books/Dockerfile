#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["API/Books/Books.csproj", "API/Books/"]
COPY ["Libraries/Models/Models.csproj", "Libraries/Models/"]
RUN dotnet restore "API/Books/Books.csproj"
COPY . .
WORKDIR "/src/API/Books"
RUN dotnet build "Books.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Books.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Books.dll"]
