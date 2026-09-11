# Cómo ejecutar el backend

## Necesitas tener instalado

- .NET SDK 10
- SQL Server (local o Express)
- La herramienta `dotnet-ef`: `dotnet tool install --global dotnet-ef`

## Pasos

1. Abre una terminal en la carpeta `backend`.

2. Crea la base de datos:
   ```powershell
   dotnet ef database update --project src/RepuestosDespacho.Infrastructure --startup-project src/RepuestosDespacho.Api
   ```

3. Levanta la API:
   ```powershell
   dotnet run --project src/RepuestosDespacho.Api --launch-profile http
   ```

4. Abre el navegador en `http://localhost:5096/swagger` para ver y probar los endpoints.

## Cómo correr las pruebas automatizadas

```powershell
dotnet test tests/RepuestosDespacho.Api.Tests
```

(Necesita que SQL Server esté corriendo, igual que en el paso 2.)
