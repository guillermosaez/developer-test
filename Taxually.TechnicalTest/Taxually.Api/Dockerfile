FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Taxually.Api/Taxually.Api.csproj", "Taxually.Api/"]
COPY ["Taxually.Application/Taxually.Application.csproj", "Taxually.Application/"]
COPY ["Taxually.Infrastructure/Taxually.Infrastructure.csproj", "Taxually.Infrastructure/"]
RUN dotnet restore "Taxually.Api/Taxually.Api.csproj"
COPY . .
WORKDIR "/src/Taxually.Api"
RUN dotnet build "./Taxually.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Taxually.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Taxually.Api.dll"]
