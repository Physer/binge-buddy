FROM mcr.microsoft.com/dotnet/sdk:7.0 as build-env
ARG SERVICE_NAME

WORKDIR /src
COPY ./ .
RUN dotnet publish ${SERVICE_NAME} -c Release -o /publish

FROM mcr.microsoft.com/dotnet/sdk:7.0 as migrations
ARG CONNECTION_STRING

WORKDIR /src
COPY ./ .
RUN dotnet tool install --global dotnet-ef
ENV PATH="/root/.dotnet/tools:${PATH}"	
ENV ConnectionStrings__BingeDatabase=${CONNECTION_STRING}
RUN dotnet ef
WORKDIR /src/Scraper
ENTRYPOINT ["dotnet", "ef", "database", "update"]

FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime
ARG SERVICE_NAME
ENV SERVICE_NAME=${SERVICE_NAME}

WORKDIR /publish
COPY --from=build-env /publish .
ENTRYPOINT dotnet ${SERVICE_NAME}.dll