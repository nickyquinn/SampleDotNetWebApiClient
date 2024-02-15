FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WebApiConnectSampleApp/WebApiConnectSampleApp.csproj", "WebApiConnectSampleApp/"]
RUN dotnet restore "WebApiConnectSampleApp/WebApiConnectSampleApp.csproj"
COPY . .
WORKDIR "/src/WebApiConnectSampleApp"
RUN dotnet build "WebApiConnectSampleApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "WebApiConnectSampleApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApiConnectSampleApp.dll"]
