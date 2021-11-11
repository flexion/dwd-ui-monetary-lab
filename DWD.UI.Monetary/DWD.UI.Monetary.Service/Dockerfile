FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["DWD.UI.Monetary.Domain/DWD.UI.Monetary.Domain.csproj", "DWD.UI.Monetary.Domain/"]
COPY ["DWD.UI.Monetary.Service/DWD.UI.Monetary.Service.csproj", "DWD.UI.Monetary.Service/"]
RUN dotnet restore "DWD.UI.Monetary.Service/DWD.UI.Monetary.Service.csproj"

COPY . .
WORKDIR "/src/DWD.UI.Monetary.Service"
RUN dotnet build "DWD.UI.Monetary.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DWD.UI.Monetary.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DWD.UI.Monetary.Service.dll"]
