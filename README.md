# OracleCMS CarStockApi

CarStockApi is a .NET Core Web API that allows car dealers to manage their car stock. 

## Description

The API provides endpoints for adding, removing, updating, and retrieving car information, as well as searching for cars by make and model.

## Features
* Add, update, and remove cars in a dealer's stock
* Retrieve the stock list for a specific dealer
* Search cars by make and model
* In-memory database for rapid testing and development
* Swagger UI for API testing and exploration

## Getting Started

### Dependencies

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker (optional, for containerization)](https://docs.docker.com/get-docker/)

### Cloning the Repository

To clone the repository, run the following command:
```
git clone https://github.com/Patrick-007/OracleCMS_Projects.git
cd OracleCMS_Projects
```
## Running the Application

1. Build the Project
Before running the application, build the project using the following command:
```
dotnet build
```
2. Run the Application
To run the application locally, use the following command:
```
dotnet run
```
This will start the API on http://localhost:5000 (HTTP). You can change the URLs in the launchSettings.json file.
3. Access the API
Once the application is running, you can access the API documentation using Swagger by navigating to:
http://localhost:5000/swagger


## Running the Project in Docker (Optional)

Any advise for common problems or issues.
```
command to run if program contains helper info
```

## Authors

- Leting Zhouli
- Email: letingzhouli@gmail.com
