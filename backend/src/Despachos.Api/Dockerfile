# Contexto de build: ./backend (ver docker-compose.yml en la raiz del repo). Se
# construye desde ahi, no desde esta carpeta, porque este Dockerfile necesita copiar
# los 4 proyectos de la solucion (Domain, Application, Infrastructure y Api son
# hermanos bajo backend/src/), no solo el de la Api.

# ---------- Etapa 1: restore + build + publish ----------
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Se copian PRIMERO solo los .csproj de los 4 proyectos (no el codigo fuente todavia),
# cada uno a su carpeta correspondiente, para que "dotnet restore" quede en su propia
# capa de Docker: mientras ningun .csproj cambie, una reconstruccion posterior
# reutiliza esta capa cacheada y no vuelve a descargar paquetes NuGet, aunque cambien
# los archivos .cs.
COPY src/Despachos.Domain/Despachos.Domain.csproj src/Despachos.Domain/
COPY src/Despachos.Application/Despachos.Application.csproj src/Despachos.Application/
COPY src/Despachos.Infrastructure/Despachos.Infrastructure.csproj src/Despachos.Infrastructure/
COPY src/Despachos.Api/Despachos.Api.csproj src/Despachos.Api/

RUN dotnet restore src/Despachos.Api/Despachos.Api.csproj

# Recien ahora se copia el resto del codigo fuente de los 4 proyectos (sobreescribe
# los mismos .csproj ya copiados con el mismo contenido, asi que no invalida el
# restore si no cambiaron).
COPY src/ src/

RUN dotnet publish src/Despachos.Api/Despachos.Api.csproj \
    -c Release \
    -o /app/publish \
    --no-restore


FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .


ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Despachos.Api.dll"]
