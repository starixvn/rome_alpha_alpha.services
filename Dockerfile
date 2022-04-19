FROM registry.nexdev.net:5050/docker/dotnet/core/sdk:6.0 as builder
WORKDIR /src

COPY src/Alpha.API/Alpha.API.csproj ./Alpha.API/
RUN dotnet nuget add source http://nuget.nexdev.net/api/v2
RUN dotnet restore Alpha.API/Alpha.API.csproj -v n

COPY src/Alpha.Customers ./Alpha.Customers
COPY src/Alpha.Reports ./Alpha.Reports
COPY src/Alpha.Shared ./Alpha.Shared
COPY src/Alpha.API ./Alpha.API

WORKDIR /src/Alpha.API
RUN dotnet publish -r linux-x64 --self-contained false -o /app

FROM registry.nexdev.net:5050/docker/dotnet/core/aspnet:6.0 as base

RUN sed -i 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/g' /etc/ssl/openssl.cnf
RUN sed -i 's/MinProtocol = TLSv1.2/MinProtocol = TLSv1/g' /etc/ssl/openssl.cnf
RUN sed -i 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/g' /usr/lib/ssl/openssl.cnf
RUN sed -i 's/MinProtocol = TLSv1.2/MinProtocol = TLSv1/g' /usr/lib/ssl/openssl.cnf

WORKDIR /app
COPY --from=builder /app .
ENV TZ="America/La_Paz"

ENTRYPOINT ["dotnet", "Alpha.API.dll"]
