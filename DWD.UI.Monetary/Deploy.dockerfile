# .NET Version
ARG sdk_ver=6.0

# Use Microsoft's official build .NET image.
# Get the full SDK to build the app. The Microsoft image uses
# a Debian linux base

# This stage will add the project structure and all the
# library dependencies from Nuget, build, and then publish
# as a single file executable.
FROM mcr.microsoft.com/dotnet/sdk:${sdk_ver} AS publish

# Setup a working directory for the source to be copied into
WORKDIR /src
COPY . .

# publish a single file executable to the out dir
RUN dotnet publish "./DWD.UI.Monetary.Service/DWD.UI.Monetary.Service.csproj" \
  --runtime linux-x64 \
  --self-contained true \
  /p:PublishTrimmed=false \
  /p:PublishSingleFile=true \
  /p:PublishReadyToRun=true \
  -c Release \
  -o /out

# Final (deployed) stage uses a new Microsoft runtime base since the application
# is now compiled and linked. Here it only needs the dotnet runtime
# dependencies.
FROM mcr.microsoft.com/dotnet/runtime-deps:${sdk_ver} AS final

# Install some help from debian so the container
# is easier to debug. The Cloud Code plugin
# uses ps and zip to attach to run process and debug in container and extract
# data. This also allows remote (production) debugging.
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
# https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-environment-variables
# https://docs.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel/endpoints?view=aspnetcore-6.0
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=0 \
  DOTNET_RUNNING_IN_CONTAINER=true \
  ASPNETCORE_URLS=http://+:8080

# run the web service on container startup
EXPOSE 8080/tcp
ENTRYPOINT ["./DWD.UI.Monetary.Service", "--urls", "http://0.0.0.0:8080"]
