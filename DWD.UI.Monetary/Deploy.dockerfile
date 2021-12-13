# .NET Version
ARG sdk_ver=6.0

# Use Microsoft's official build .NET image.
# Get the full SDK to build the app. The Microsoft image uses
# a Debian linux base

# This layer will add the project structure and all the
# library dependencies from Nuget.
FROM mcr.microsoft.com/dotnet/sdk:${sdk_ver} AS build

# Setup  a working directory for the source to be copied into
WORKDIR /src
COPY . .

# restore all the dependencies
RUN dotnet restore -p:PublishReadyToRun=true -p:PublishTrimmed=false "DWD.UI.Monetary.Service/DWD.UI.Monetary.Service.csproj"

# publish layer takes all the previous build steps from cache
# and runs a publish for the final artifacts
FROM build AS publish
WORKDIR /src

# publish as a single file executable
RUN dotnet publish "./DWD.UI.Monetary.Service/DWD.UI.Monetary.Service.csproj" \
  --runtime linux-x64 \
  --self-contained true \
  --no-restore \
  /p:PublishTrimmed=false \
  /p:PublishSingleFile=true \
  /p:PublishReadyToRun=true \
  -c Release \
  -o /out

# Final (deployed) layer a new Microsoft runtime base since the application
# is now compiled and linked. Here it only needs the dotnet runtime
# dependencies.
FROM mcr.microsoft.com/dotnet/runtime-deps:${sdk_ver} AS final

# Install some help from debian so the container
# is easier to debug.
RUN apt-get update && apt-get install -y unzip procps

# Set up a user so the application does not run as root.
RUN adduser \
  --disabled-password \
  --home /app \
  --gecos '' app \
  && chown -R app /app
USER app

# Copy the published binary to a run directory in the
# deployable container
WORKDIR /run
COPY --from=publish /out .

# set up the dotnet environmnet vars
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=0 \
  DOTNET_RUNNING_IN_CONTAINER=true \
  ASPNETCORE_URLS=http://+:8080

# run the web service on container startup
EXPOSE 8080/tcp
ENTRYPOINT ["./DWD.UI.Monetary.Service", "--urls", "http://0.0.0.0:8080"]
