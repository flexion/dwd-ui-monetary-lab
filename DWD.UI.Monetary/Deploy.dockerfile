# SDK Version
ARG sdk_ver=5.0-alpine
# Use Microsoft's official build .NET image.
FROM mcr.microsoft.com/dotnet/sdk:${sdk_ver} AS restore
WORKDIR /app

ARG arg_domain_dir=DWD.UI.Monetary.Domain
ARG arg_service_dir=DWD.UI.Monetary.Service
ENV env_domain_dir=${arg_domain_dir}
ENV env_service_dir=${arg_service_dir}

WORKDIR /src

# throw in a dependency layer
RUN mkdir ${env_domain_dir} ${env_service_dir}
COPY ./${env_domain_dir}/*.csproj ${env_domain_dir}/
COPY ./${env_service_dir}/*.csproj ${env_service_dir}/
RUN dotnet restore "DWD.UI.Monetary.Service/DWD.UI.Monetary.Service.csproj"

FROM restore AS build
COPY . .
WORKDIR "/src/DWD.UI.Monetary.Service"
RUN dotnet build "DWD.UI.Monetary.Service.csproj" -c Release -o /app/build --no-restore

FROM build AS publish
WORKDIR "/src/DWD.UI.Monetary.Service"

# publish layer
RUN dotnet publish "DWD.UI.Monetary.Service.csproj" \
  --runtime alpine-x64 \
  --self-contained true \
  --no-restore \
  /p:PublishTrimmed=false \
  /p:PublishSingleFile=true \
  /p:PublishReadyToRun=true \
  -c Release \
  -o /app/publish

# build out runtime image
FROM mcr.microsoft.com/dotnet/runtime-deps:${sdk_ver}
# set up security
RUN adduser \
  --disabled-password \
  --home /app \
  --gecos '' app \
  && chown -R app /app
USER app
WORKDIR /app

# copy in application layer
COPY --from=publish /app/publish .

# set up to environment
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1 \
  DOTNET_RUNNING_IN_CONTAINER=true \
  ASPNETCORE_URLS=http://+:8080

# run the web service on container startup
EXPOSE 8080/tcp
ENTRYPOINT ["./DWD.UI.Monetary.Service", "--urls", "http://0.0.0.0:8080"]
