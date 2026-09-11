# Cómo ejecutar todo con Docker

Levanta los 3 componentes juntos (SQL Server, backend y frontend), sin necesitar .NET, Node ni SQL Server instalados.

## Necesitas tener instalado

- Docker Desktop, abierto y corriendo

## Pasos

1. Abre una terminal en la raíz del proyecto.

2. Copia el archivo de variables de entorno:
   ```powershell
   copy .env.example .env
   ```

3. Abre `.env` y cambia `SA_PASSWORD` por una contraseña propia (mínimo 8 caracteres, con mayúsculas, minúsculas, números y algún símbolo).

4. Levanta todo:
   ```powershell
   docker compose up --build -d
   ```

5. Abre el navegador en `http://localhost:8081`.

## Para apagar todo

```powershell
docker compose down
```
