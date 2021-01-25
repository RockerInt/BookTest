# Books

Este es un proyecto de prueba para manejo de libros, autores y editoriales que implementa *.NET 5.0, SQL Server 2019 y docker*.


## Build & Test

Se debe clonar el repositorio y en la raíz de proyecto ejecutar:

> docker-compose up -d

Luego se debe conectar a la base de datos y ejecutar el archivo *BD/Books/Restore/create.sql*

Estos son los servicios que corren en el docker-compose:

- WebAPP: http://localhost:8080, https://localhost:44399
- Books(API): http://localhost:8081, https://localhost:44398
- Gateway: http://localhost:8082, https://localhost:44397
- SqlServer: "Server=localhost;User Id=sa;Password=12345678a"

## Arquitectura

El proyecto tiene una *WebApp* **ASP<!-- -->.NET Core MVC** que se conecta a un *WebAPI REST* construido con **ASP<!-- -->.NET Core** el cual usa **Entity Framework Core** para interactuar con una base de datos **SQL Server 2019**.

También cuenta con un *Gateway WebAPI REST* construido con **ASP<!-- -->.NET Core** que se conecta de igual manera con el *WebAPI* mencionado anteriormente.

## Repository

https://github.com/RockerInt/BookTest

## License
Code released under the [MIT license](https://opensource.org/licenses/MIT).
