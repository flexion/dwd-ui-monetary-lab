# SDK Version
ARG sdk_ver=6.0
# Use Microsoft's official build .NET image.
FROM mcr.microsoft.com/dotnet/aspnet:${sdk_ver} AS base
WORKDIR /src

FROM mcr.microsoft.com/dotnet/sdk:${sdk_ver} AS build
WORKDIR /src
COPY . .
RUN dotnet restore "DWD.UI.Monetary.Service/DWD.UI.Monetary.Service.csproj"
RUN dotnet build "./DWD.UI.Monetary.Service/DWD.UI.Monetary.Service.csproj" -c Release -o /out

FROM build AS publish
RUN dotnet publish "DWD.UI.Monetary.Service/DWD.UI.Monetary.Service.csproj" -c Release -o /out

# Building final image used in running container
FROM base AS final
RUN apt-get update \
    && apt-get install -y unzip procps
WORKDIR /src
COPY --from=publish /out .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet DWD.UI.Monetary.Service.dll
