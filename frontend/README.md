# Institutions

Institutions frontend application. 
- Framework: Angular
- Nx version 16.10.0
- Node version: 20.17.0 

Backend Swagger: http://institutionsservice.westeurope.azurecontainer.io:8080/swagger/index.html

Frontend: http://institutionfrontend.westeurope.azurecontainer.io:4200/


# Project structure

```
Institutions
├── apps/                   # Applications subfolder
│   ├── host                # Nx module federation Host application
│   ├── host-e2e            # Generated e2e tests
│   ├── institutions        # Nx module federation Remote application
│   ├── institutions-e2e    # Generated e2e tests
├── libs/                   # Shared libs
│   ├── services/           # Services for backend requests
│   ├── shared/             # Shared view components
```


## Install

Navigate to project root folder.

Run command:
`cmd> npm install --legacy-peer-deps`

## Run

### Run host

Run command:
`cmd> npx nx serve host`

Local url: http://localhost:4200/

### Run remote
Run command:
`cmd> npx nx serve institutions`

Local url: http://localhost:4201/

### Run both - host and remote

Run command:
`cmd> npx nx serve host --devRemotes=institutions`

Local host url: http://localhost:4200/

Local remote url: http://localhost:4201/


## Configuration

### Development url

1. Navigate to file: /libs/services/lib/services/service.base.ts
2. Change development url if needed:
```
export class ServiceBase {
  backendUrl: string = isDevMode() ? "http://localhost:7255" : "/api";
}
```

### Backend proxy url
1. Navigate to file nginx.conf
2. Change backend proxy url if needed:
```
   proxy_pass http://host.docker.internal:7255;  # Backend API URL
```

## Docker

### Build

Run command:

`cmd> docker build . -t nx-microfrontend`

### Run
`cmd> docker run -p 4200:4200 -p 4201:4201 --name Frontend nx-microfrontend`