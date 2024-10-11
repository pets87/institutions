
# InstitutionsService

Webservice for institution specific operations. 
- Framework: dotnet 8.0.0 
- SDK: 8.0.303 
- Runtime: 8.0.7

Swagger: http://institutionsservice.westeurope.azurecontainer.io:8080/swagger/index.html

Frontend: http://institutionfrontend.westeurope.azurecontainer.io:4200/


# Project structure

```
PersonService/
├── Controllers/                               # API controllers
│   ├── AddressController.cs                   # Controller for Address related operations
│   ├── ClassifierController.cs                # Controller for Classifier related operations
│   ├── InstitutionController.cs               # Controller for Institution related operations
│   ├── InstitutionReplicationController.cs    # Controller for InstitutionReplication related operations
│   ├── TranslationController.cs               # Controller for Translation related operations   
├── Data/                                      # Application database related files
│   ├── Interceptors/                          # For entity modification before save
├── Middlewares/                               # For request and response logging
├── Models/                                    # Database described entities
├── RabbitMQ/                                  # For messaging
├── Services/                                  # Service layer, that contains service interfaces
│   ├── Impl/                                  # Implementation of service interfaces
├── Util/                                      # Shared helper utilities
├── Validators/                                # Validators for http requests
│   ├── Institution/                           # Institutionrelated validators
│   ├── InstitutionReplication/                # InstitutionReplication validators
│   ├── Translation/                           # Translation validators
├── appsettings.json                           # Settings file for running application
├── Program.cs                                 # Application startup file
```


## Build

Prerequisities:
 - dotnet 8.0.0 or newer must be installed. https://dotnet.microsoft.com/en-us/download

Navigate to project root folder.

Run command:

`cmd> dotnet build`

## Run
Navigate to project InstitutionsService/ folder.

Run command:

`cmd> dotnet run`

Local OpenApi (Swagger) url: http://localhost:7255/swagger/index.html

## Configuration
No configuration 

Uses Entity Framework Core In-Memory database. 

## Test
Navigate to project root folder.

Run command:

`cmd> dotnet test`

## Test Coverage
Prerequisities
- dotnet-reportgenerator-globaltool must be installed globally

**Manual report generation**

Run command:

`cmd> dotnet tool install -g dotnet-reportgenerator-globaltool`
 
Run test with coverage:

`cmd> dotnet test /p:CollectCoverage=true --collect:"XPlat Code Coverage"`

Run report generator:

`cmd> reportgenerator -reports:".\InstitutionsService.Tests\TestResults\{guid}\coverage.cobertura.xml" -targetdir:"coverage" -reporttypes:Html`

Report will be create inside:

`.\InstitutionsService.Tests\TestResults\Coverage\index.html`


**Automatic report generation**

Naviagte to root folder.
Run all at once:

`cmd> test-with-coverage.bat`

This will run all previous commands at once:
1. Install dotnet-reportgenerator-globaltool if not installed
2. Run tests with coverage
3. Generates report

Report will be create inside:

`.\InstitutionsService.Tests\TestResults\Coverage\index.html`

