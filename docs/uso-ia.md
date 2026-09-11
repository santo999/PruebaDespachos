# Registro breve de uso de IA

Usé Claude como apoyo durante el desarrollo de esta prueba técnica. Aquí dejo un resumen simple de para qué lo usé, qué le pregunté, y cómo validé lo que me propuso antes de dejarlo en el proyecto.

## Para qué lo usé

Lo usé principalmente para discutir decisiones de diseño antes de aplicarlas, sobre todo en las partes más delicadas del enunciado (evitar despachos duplicados y manejar la concurrencia).

## Algunas de las preguntas que le hice

**Cómo evitar código repetitivo en los controladores.** Le pregunté si convenía poner un `try/catch` en cada controlador para manejar los errores, o si había una forma de no repetir esa lógica en cada uno. Me explicó las dos opciones y sus ventajas, y terminamos usando un middleware central (`IExceptionHandler`) que atrapa las excepciones una sola vez y decide qué código HTTP devolver, en vez de repetir el mismo bloque de código en cada controlador.

**Cuál es la mejor práctica para centralizar el manejo de errores.** Relacionado con lo anterior, le pregunté directamente cuál era la práctica recomendada en .NET utilizando clean architecture. Me confirmó que centralizar el manejo de excepciones en un solo lugar (en vez de manejarlas dispersas por todo el código) es el enfoque estándar, y así fue como quedó implementado.

**Alternativas para que un despacho no se registrara dos veces.** Le pedí que me explicara distintas formas de evitar que la misma solicitud. Revisamos varias opciones (validar antes de insertar, usar una restricción única en la base de datos, entre otras) hasta llegar a la que quedó en el proyecto: una clave única en la base de datos combinada con una validación específica del error que devuelve SQL Server cuando esa clave ya existe.

**Cómo levantar todo con Docker.** No había trabajado con Docker hace bastante tiempo, así que le pedí que me guiara paso a paso para poder correr el backend, el frontend y la base de datos juntos. Me explicó qué era cada archivo que estábamos creando (Dockerfile, docker-compose.yml, el archivo `.env`) y me acompañó mientras lo iba probando en mi máquina.

## Cómo validé lo que me propuso

No dejé nada por confiar de más: antes de dar por buena cualquier parte del código, la probé de verdad. Compilé el proyecto varias veces, probé los endpoints manualmente (registrar un despacho, repetir la misma referencia, pedir más cantidad de la disponible), y ya al final levanté los tres contenedores con Docker en mi propia máquina y confirmé que todo funcionara junto antes de darlo por terminado.

## Algo que descartamos en el camino

En un momento consideramos usar Testcontainers (una herramienta que crea una base de datos de prueba dentro de un contenedor Docker solo para las pruebas automatizadas). Al final no la usamos, porque hubiera obligado a cualquiera que quisiera correr las pruebas a tener Docker instalado, incluso si esa persona prefiere ejecutar todo de forma local sin Docker. Terminamos con una opción más simple que no agrega esa dependencia extra.
