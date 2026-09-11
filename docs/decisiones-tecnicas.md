# Decisiones técnicas relevantes

Versión corta de las decisiones más importantes que tomé en el backend.

## 1. Índice único en la referencia externa (evita despachos duplicados)

A la columna `ReferenciaExterna` de la tabla `Despachos` le puse un índice único en la base de datos. Así, si la misma referencia llega dos veces (por ejemplo, porque el usuario le dio doble clic o hubo un reintento de red), SQL Server rechaza el segundo `INSERT` directamente — no depende de que mi código lo valide bien, la base de datos lo garantiza.

## 2. `UPDATE` condicional para descontar el stock (evita quedar en negativo)

En vez de leer el stock, revisarlo en código, y después actualizarlo (lo cual puede fallar si dos peticiones llegan casi al mismo tiempo), hago un solo `UPDATE` con una condición en el `WHERE`:

```sql
UPDATE Repuestos SET CantidadDisponible = CantidadDisponible - @cantidad
WHERE Id = @id AND CantidadDisponible >= @cantidad
```

Si no hay stock suficiente en ese momento exacto, el `UPDATE` no afecta ninguna fila y me entero por el número de filas afectadas. Esto evita la condición de carrera sin tener que bloquear nada manualmente.

## 3. Un middleware central para manejar los errores (`IExceptionHandler`)

En vez de poner un `try/catch` repetido en cada controlador, creé una sola clase (`AppExceptionHandler`) que atrapa las excepciones de negocio y decide qué código HTTP devolver (404, 409, etc.). Los controladores quedan limpios, solo llaman al servicio y devuelven el resultado — si algo falla, el middleware se encarga.

## 4. Validaciones básicas con `DataAnnotations` en el request

En el objeto que recibe el `POST` le puse atributos simples para validar antes de que la petición llegue a la lógica de negocio:

```csharp
[Required(AllowEmptyStrings = false), StringLength(100, MinimumLength = 1)]
string ReferenciaExterna,

[Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que cero.")]
int Cantidad
```

Esto rechaza automáticamente (con un `400`) los casos obvios — campo vacío, cantidad negativa o cero — sin que yo tenga que escribir esos `if` a mano.

## 5. Detectar el error específico de duplicado, no cualquier error

Cuando el `INSERT` del despacho falla, no asumo que cualquier error significa "referencia duplicada". Reviso el código de error exacto que devuelve SQL Server para una violación de índice único:

```csharp
catch (DbUpdateException ex) when (EsViolacionDeUnicidad(ex))
```

Así, si el `INSERT` falla por otra razón (un bug real, por ejemplo), no lo confundo con un duplicado y sigue apareciendo como el error 500 que realmente es, en vez de esconderlo.
