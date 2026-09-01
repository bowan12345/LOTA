FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY LOTAWeb/LOTAWeb.csproj LOTAWeb/
COPY LOTA.Model/LOTA.Model.csproj LOTA.Model/
COPY LOTA.Service/LOTA.Service.csproj LOTA.Service/
COPY LOTA.Utility/LOTA.Utility.csproj LOTA.Utility/
COPY LOTA.DataAccess/LOTA.DataAccess.csproj LOTA.DataAccess/
RUN dotnet restore LOTAWeb/LOTAWeb.csproj

COPY . .
RUN dotnet publish LOTAWeb/LOTAWeb.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "LOTAWeb.dll"]
