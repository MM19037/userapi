# UserApi

API REST simple para la gestión de usuarios, construida con ASP.NET Core (.NET 10).

## Descripción

El proyecto expone un CRUD básico de usuarios almacenados en memoria mediante un servicio Singleton (`UsuarioSingletonService`), respaldado por un `ConcurrentDictionary` para permitir acceso seguro entre múltiples hilos. Al iniciar, se cargan dos usuarios de ejemplo.

## Tecnologías

- .NET 10 / ASP.NET Core Web API
- Controllers (`ApiController`)
- OpenAPI (`Microsoft.AspNetCore.OpenApi`) para documentación de la API en desarrollo

## Estructura del proyecto

```
Controllers/
  UsuariosController.cs     Endpoints HTTP de usuarios
Models/
  Usuario.cs                Modelo de datos (Id, Nombre, Email)
Services/
  IUsuarioService.cs         Contrato del servicio de usuarios
  UsuarioSingletonService.cs Implementación en memoria (Singleton)
Program.cs                  Configuración y arranque de la aplicación
```

## Cómo ejecutar

```bash
dotnet run
```

La API queda disponible en:

- HTTP: `http://localhost:5038`
- HTTPS: `https://localhost:7192`

En entorno de desarrollo, el documento OpenAPI se expone en `/openapi/v1.json`.

## Endpoints

Base route: `api/usuarios`

| Método | Ruta               | Descripción                          | Respuestas                                  |
|--------|---------------------|---------------------------------------|----------------------------------------------|
| GET    | `/api/usuarios`      | Obtiene la lista de todos los usuarios | `200 OK`                                      |
| GET    | `/api/usuarios/{id}` | Obtiene un usuario por su Id           | `200 OK` / `404 Not Found`                    |
| POST   | `/api/usuarios`      | Crea un nuevo usuario                  | `201 Created` / `400 Bad Request`             |

### Modelo `Usuario`

```json
{
  "id": 0,
  "nombre": "string",
  "email": "string"
}
```

- `Id`: asignado automáticamente por el servidor (autoincremental).
- `Nombre`: obligatorio; si viene vacío o nulo, la creación devuelve `400 Bad Request`.
- `Email`: opcional a nivel de validación del servidor.

### Ejemplos

**Obtener todos los usuarios**

```
GET /api/usuarios
```

**Obtener un usuario por Id**

```
GET /api/usuarios/1
```

**Crear un usuario**

```
POST /api/usuarios
Content-Type: application/json

{
  "nombre": "Ana Torres",
  "email": "ana@empresa.com"
}
```

## Notas

- Los datos se almacenan únicamente en memoria: se reinician cada vez que la aplicación se detiene o reinicia.
- No hay persistencia en base de datos ni autenticación implementada.
