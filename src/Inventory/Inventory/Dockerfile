FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5054

ENV ASPNETCORE_URLS=http://+:5054

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["Inventory/Inventory.csproj", "Inventory/"]
RUN dotnet restore "Inventory/Inventory.csproj"
COPY . .
WORKDIR "/src/Inventory"
RUN dotnet build "Inventory.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Inventory.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Inventory.dll"]
