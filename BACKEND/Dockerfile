FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar solución y proyectos
COPY ./PRODUCTOS_COMPRAS_FULL_STACK.sln ./
COPY ./PRODUCTOS_COMPRAS_FULL_STACK ./PRODUCTOS_COMPRAS_FULL_STACK

# Restaurar dependencias
RUN dotnet restore

# Publicar
RUN dotnet publish PRODUCTOS_COMPRAS_FULL_STACK/PRODUCTOS_COMPRAS_FULL_STACK.csproj -c Release -o out

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "PRODUCTOS_COMPRAS_FULL_STACK.dll"]