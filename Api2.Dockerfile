FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/OpenTelemetryExample.Api2/OpenTelemetryExample.Api2.csproj", "src/OpenTelemetryExample.Api2/"]
COPY ["src/OpenTelemetryExample.Core/OpenTelemetryExample.Core.csproj", "src/OpenTelemetryExample.Core/"]
RUN dotnet restore "src/OpenTelemetryExample.Api2/OpenTelemetryExample.Api2.csproj"
COPY . .
WORKDIR "/src/src/OpenTelemetryExample.Api2"
RUN dotnet build "OpenTelemetryExample.Api2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OpenTelemetryExample.Api2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OpenTelemetryExample.Api2.dll"]