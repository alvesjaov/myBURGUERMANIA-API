# Use a imagem base do .NET SDK
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 8080

# Use a imagem base do .NET SDK para buildar a aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["myBURGUERMANIA-API.csproj", "./"]
RUN dotnet restore "myBURGUERMANIA-API.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "myBURGUERMANIA-API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "myBURGUERMANIA-API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "myBURGUERMANIA-API.dll"]