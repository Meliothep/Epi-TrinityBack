# Utilise une image de base ASP.NET pour l'exécution
FROM mcr.microsoft.com/dotnet/sdk:9.0@sha256:3fcf6f1e809c0553f9feb222369f58749af314af6f063f389cbd2f913b4ad556 AS base
ARG CONFIGURATION=Debug
ARG SERVICE=Inventory
ARG PORT=8080

WORKDIR /app
EXPOSE $PORT

# Configure l'URL de l'application
ENV ASPNETCORE_URLS=http://+:$PORT
ENV SCALAR_URLS=http://localhost:$PORT

# Change l'utilisateur pour éviter les permissions root
USER app

# Étape de construction
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0@sha256:3fcf6f1e809c0553f9feb222369f58749af314af6f063f389cbd2f913b4ad556 AS build
ARG CONFIGURATION=Debug
ARG SERVICE=Inventory
ARG PORT=8080

WORKDIR /src
# Copier les fichiers .csproj avec leur structure correcte
COPY ./Services/${SERVICE} ./Services/${SERVICE}
COPY ./Commons ./Commons


# Restaurer les dépendances
WORKDIR "/src"
RUN dotnet restore "Services/${SERVICE}/${SERVICE}.csproj"

# Construire le projet
RUN dotnet build "/src/Services/${SERVICE}/${SERVICE}.csproj" -c $CONFIGURATION -o /app/build

# Étape de publication
FROM build AS publish
RUN dotnet publish "/src/Services/${SERVICE}/${SERVICE}.csproj" -c $CONFIGURATION -o /app/publish /p:UseAppHost=false

# Étape finale : préparation de l'image pour l'exécution
FROM base AS final
USER root
WORKDIR /app
COPY --from=publish /app/publish .
RUN ln -s "${SERVICE}.dll" docker_service_name.dll
ENTRYPOINT ["dotnet", "docker_service_name.dll"]