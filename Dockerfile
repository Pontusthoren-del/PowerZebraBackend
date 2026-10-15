# --- Build-steg ---
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["Meetly.Api/Meetly.Api.csproj", "Meetly.Api/"]
RUN dotnet restore "Meetly.Api/Meetly.Api.csproj"
COPY . .
WORKDIR "/src/Meetly.Api"
RUN dotnet build "Meetly.Api.csproj" -c Release -o /app/publish

# --- Publicera ---
FROM build AS publish
RUN dotnet publish "Meetly.Api/Meetly.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# --- Runtime-steg ---
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "Meetly.Api.dll"]