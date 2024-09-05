## Alicunde Test

## Project Description

The application consumes an external API, stores the data in a database and exposes it through a RESTAPI with Swagger and Docker.

## Technologies used

- .NET - C#
- SQL Server
- MVC

## Project Configuration and Execution

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) installed on your machine.
- [Docker](https://www.docker.com/get-started) installed and running, with a SQL Server instance configured.

### Secret Management

The sensitive project data is stored in an environment variable, but the possibility to use the data locally has been left implemented.

In this environment variable it is necessary to modify the connection string by setting the IP corresponding to the database host.

#### Downloading and running the project

1. Clone this repository.
2. Modify the Dockerfile to find the `DB_CONNECTION_STRING` environment variable and set its corresponding ip value.
3. Have Docker Desktop running on the host machine.
3. Have the SQL Server services running and a user and password corresponding to those of Dockerfile otherwise modify them as well.
3. Compile and run the application using your IDE tools or the `dotnet run Docker` command to use the Docker profile created.

## Techniques used

- **SW Patterns:** The API uses the default Swagger view, but the MVC has been used. 
- **Exceptions:** Repetitive `try-catch` code blocks have been used due to lack of knowledge of the MONAD pattern.
- **Documentation:** The API is documented using Swagger.
- **Tests:** No unit tests have been done, but when executing the code you can see how all the endpoints work and the database is updated correctly.

# Prueba Alicunde

## Descripción del Proyecto

La aplicación consume una API externa, almacena los datos en una base de datos y los expone a través de una RESTAPI con Swagger y Docker.

## Tecnologías utilizadas

- .NET - C#
- SQL Server
- MVC

## Configuración y Ejecución del Proyecto

### Requisitos Previos

- [.NET SDK](https://dotnet.microsoft.com/download) instalado en tu máquina.
- [Docker](https://www.docker.com/get-started) instalado y corriendo, con una instancia de SQL Server configurada.

### Gestión de Secretos

Los datos sensibles del proyecto estan almacenados en una variable de entorno, aun asi se ha dejado implementada la posibilidad de utilizar los datos de manera local.

En dicha variable de entorno es necesario modificar la cadena de conexión colocando la IP correspondiente al host de la base de datos.

#### Descarga y ejecución del proyecto

1. Clona este repositorio.
2. Modificar el archivo Dockerfile para encontrar la variable de entorno `DB_CONNECTION_STRING` y poner su valor de ip correspondiente.
3. Tener Docker Desktop corriendo en la máquina host.
3. Tener los servicios de SQL Server corriendo y un usuario y contraseña correspondientes a los de Dockerfile de lo contrario modificarlos tambien.
3. Compila y ejecuta la aplicación usando las herramientas de tu IDE o el comando `dotnet run Docker` para utilizar el perfil Docker creado.

## Técnicas empleadas

- **Patrones SW:** La API utiliza la vista por defecto de Swagger, pero se ha utilizado el MVC como patron de desarrollo.
- **Excepciones:** Se han utilizado bloques de codigo `try-catch` repetitivos por desconocimiento del patron MONAD.
- **Documentación:** La API está documentada utilizando Swagger.
- **Pruebas:** No se han hecho pruebas unitarias, pero al ejecutar el codigo se puede observar como todos los endpoints funcionan y la base de datos se actualiza correctamente.


