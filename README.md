# Technical Test Alicunde

## Description
This repository contains a technical test for Alicunde, which requests the following:

1. Create a program in .NET - C# that meets the following requirements:
    - Fork the project from https://github.com/dlozanoalicunde/PTANetBackMVC.
    - Consume the following API: https://api.opendata.esett.com/. Choose only one service provided by the API.
    - Store the retrieved information in a database. (Use SQL Server in a Docker container for this.)
    - Implement a controller that allows filtering by Primary Key in the database.
    - Build a REST API with Swagger that allows viewing the data stored in the database.
    - Use Docker containers for both the database and the app itself.
    - Use MVC architecture (API only; imagine there’s a second project with the frontend, so the views will be DTOs).
    - Submit a pull request with your full name and add any comments you think are necessary for the technical evaluator.
    - Choose between implementing CRUD or CQRS.

## Observations
When given the option to use CQRS or CRUD, I chose CRUD for time optimization, as setting up a database in a container is required. 
The main goal of using responsibility segregation (read/write) is to separate databases to achieve high availability.
In this case, since only one database is being used and it's being mounted via containers on the same server as the API, I believe it's more optimal to use a simple CRUD.

## Build Instructions
1. Clone this repository

2. Add .env file in root with this values:
	SA_PASSWORD=[any password from 8 caracters containing uppercase letters, lower letters, numbers, symbols]

3. Install Docker on the target machine and run the following command:

docker-compose up --build

## Technologies Used
- .NET - C#
- SQL Server
- MVC

###############################################################################################################################

# Prueba Técnica Alicunde

## Descripción

Este repositorio contiene una prueba técnica para Alicunde en la que se solicita lo siguente:

1. Realizar un programa en .NET - C# que cumpla con los siguientes requisitos:
    - Hacer un fork del proyecto https://github.com/dlozanoalicunde/PTANetBackMVC
    - Consumir la siguiente API: [https://api.opendata.esett.com/](https://api.opendata.esett.com/). Escoge sólo 1 servicio cualquiera de los proporcionados por la API.
    - Almacenar la información obtenida en la base de datos. (usa SQL Server en contenedor de docker para esto)
    - Implementar un controlador que permita filtrar por Primary Key en la base de datos.
    - Construir una API REST con Swagger que permita visualizar los datos almacenados en la base de datos.
    - Usar contenedores Docker para DDBB y la propia App
    - Usa arquitectura MVC (sólo API imagina que existe un segundo proyecto con el frontend, por tanto las vistas serán DTOs)
    - Haz un pull request con tu nombre completo y comenta lo que creas necesario al evaluador técnico.
    - Elige entre implementar CRUD o CQRS
	
##Observaciones

Al dar la opción de utilizar CQRS o CRUD he escogido CRUD por optimización de tiempos ya que se solicita levantar una BD en un contenedor
y el principal objetivo de utilizar la segregación de responsabilidad lectura/escritura es separar las BBDD para tener alta disponibilidad,
en este caso al utilizarse una sola BD y montarla mediante contenedores en el mismo servidor que el API creo que es más optimo utilizar un CRUD simple.
	
## Instrucciones de compilación
1. Clonar este repositorio

2. Añadir un fichero .env en la carpeta raiz con estos valores:
	SA_PASSWORD=[cualquier contraseña a partir de 8 caracteres que contenga letras mayúsculas, letras minúsculas, numeros, simbolos]

3. Instalar Docker en la maquina destino y lanzar el siguiente comando:

docker-compose up --build

## Tecnologías utilizadas

- .NET - C#
- SQL Server
- MVC


