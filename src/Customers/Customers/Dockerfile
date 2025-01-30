# Utilise une image de base ASP.NET pour l'exécution
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5054

# Configure l'URL de l'application
ENV ASPNETCORE_URLS=http://+:5054

# Change l'utilisateur pour éviter les permissions root
USER app

# Étape de construction
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG configuration=Release
WORKDIR /src

# Copier les fichiers .csproj avec leur structure correcte
COPY ["src/Customers/Customers/Customers.csproj", "Customers/Customers/"]
COPY ["src/Commons/Trinity.ServiceDefaults/Trinity.ServiceDefaults.csproj", "Commons/Trinity.ServiceDefaults/"]
COPY ["src/Commons/Trinity.EntityModels/Trinity.EntityModels.csproj", "Commons/Trinity.EntityModels/"]

# Restaurer les dépendances
RUN dotnet restore "Customers/Customers.csproj"

# Copier tous les fichiers sources
COPY . .

# Construire le projet
WORKDIR "/src/Customers"
RUN dotnet build "Customers.csproj" -c $configuration -o /app/build

# Étape de publication
FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Customers.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

# Étape finale : préparation de l'image pour l'exécution
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Customers.dll"]