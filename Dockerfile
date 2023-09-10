FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY ./*.csproj .
RUN dotnet restore --use-current-runtime  

# copy everything else and build app
COPY ./. .
RUN dotnet publish --use-current-runtime --self-contained false --no-restore -o /app

# final stage/image
#FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine
FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine
WORKDIR /app
COPY --from=build /app .

RUN dotnet dev-certs https

ENTRYPOINT ["./unvest_transactions_ms"]

EXPOSE 80 443
