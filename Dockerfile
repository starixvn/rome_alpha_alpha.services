FROM hub.nexdev.net/base/dotnet/core/sdk:6.0-alpine as builder
WORKDIR /src

COPY src/Alpha.API/Alpha.API.csproj ./Alpha.API/
RUN dotnet nuget add source http://nuget.nexdev.net/api/v2
RUN --mount=type=cache,target=/root/.nuget/packages \
    dotnet restore Alpha.API/Alpha.API.csproj -v n

COPY src/Alpha.Customers ./Alpha.Customers
COPY src/Alpha.Reports ./Alpha.Reports
COPY src/Alpha.Shared ./Alpha.Shared
COPY src/Alpha.API ./Alpha.API

WORKDIR /src/Alpha.API
RUN --mount=type=cache,target=/root/.nuget/packages \
    dotnet publish -r linux-x64 --self-contained false -o /app

FROM hub.nexdev.net/base/dotnet/core/aspnet:6.0-alpine-25h1 as base

# Don’t run as root
RUN adduser --disabled-password \
    --home /app \
    --gecos '' dotnetuser && chown -R dotnetuser /app && chown -R dotnetuser /tmp
USER dotnetuser
#

WORKDIR /app
COPY --from=builder /app .
ENV TZ="America/La_Paz"

ENTRYPOINT ["dotnet", "Alpha.API.dll"]
