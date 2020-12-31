FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env

#Set workdir
WORKDIR /app

#Copy csproj and restore as distinct layers
COPY . ./

ENV DOTNET_SYSTEM_NET_HTTP_USESOCKETSHTTPHANDLER=0

RUN dotnet restore ./Cresce.Api.sln

RUN dotnet publish -c Release -o out

#Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime

WORKDIR /app

COPY --from=build-env /app/out .

RUN apt-get update -y && apt-get upgrade -y && apt-get install curl -y

HEALTHCHECK --interval=5s --timeout=3s --retries=5 \
  CMD curl http://localhost/ || exit 1

ENTRYPOINT ["dotnet", "Cresce.Api.dll"]
