# SDK Version
ARG sdk_ver=6.0
# Use Microsoft's official build .NET image.
FROM mcr.microsoft.com/dotnet/runtime-deps:${sdk_ver} AS base
WORKDIR /src

FROM mcr.microsoft.com/dotnet/sdk:${sdk_ver} AS build
WORKDIR /src
COPY . .
RUN dotnet restore -p:PublishReadyToRun=true -p:PublishTrimmed=false "DWD.UI.Monetary.Service/DWD.UI.Monetary.Service.csproj"
# RUN dotnet build "./DWD.UI.Monetary.Service/DWD.UI.Monetary.Service.csproj" -c Release -o /out

# publish layer
FROM build AS publish
WORKDIR /src

RUN dotnet publish "./DWD.UI.Monetary.Service/DWD.UI.Monetary.Service.csproj" \
  --runtime linux-x64 \
  --self-contained true \
  --no-restore \
  /p:PublishTrimmed=false \
  /p:PublishSingleFile=true \
  /p:PublishReadyToRun=true \
  -c Release \
  -o /out

# Building final image used in running container
FROM base AS final

# install some help from debian
RUN apt-get update && apt-get install -y unzip procps

# set up security
RUN adduser \
  --disabled-password \
  --home /app \
  --gecos '' app \
  && chown -R app /app
USER app

WORKDIR /src

COPY --from=publish /out .

# set up to environment
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=0 \
  DOTNET_RUNNING_IN_CONTAINER=true \
  ASPNETCORE_URLS=http://+:8080

# run the web service on container startup
EXPOSE 8080/tcp
ENTRYPOINT ["./DWD.UI.Monetary.Service", "--urls", "http://0.0.0.0:8080"]
