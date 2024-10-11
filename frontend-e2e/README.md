
# Institutions e2 tests

Institutions frontend e2e tests. 
- Framework: Cypress
- Cypress version 13.15.0
- Node version: 20.17.0 

Backend Swagger: http://institutionsservice.westeurope.azurecontainer.io:8080/swagger/index.html

Frontend: http://institutionfrontend.westeurope.azurecontainer.io:4200/


# Project structure

```
cypress
├── e2e/                            # Tests subfolder
│   ├── 01_translations.cy.js       # Translations tests
│   ├── 02_institutions.cy.js       # Institutions tests
│   ├── 03_rabbitmq.cy.js           # RabbitMQ tests
├── videos/                         # Generated test videos
│   ├── 01_translations.cy.js.mp4   # Translations tests result video
│   ├── 02_institutions.cy.js.mp4   # Institutions tests result video
│   ├── 03_rabbitmq.cy.js.mp4       # RabbitMQ tests result video
```


## Install

Navigate to project root folder.

Run command:
`cmd> npm install --legacy-peer-deps`

## Run

### Run cypress UI

Run command:
`cmd> npx cypress open`


### Run tests

Run command:
`cmd> npx cypress run --config video=true,videoCompression=0`

Outputs test results in console and  videos into /videos folder


## Configuration

### Base url

1. Navigate to file: /cypress.config.js
2. Change base url if needed (along with other settings):
```
module.exports = defineConfig({
  e2e: {
    baseUrl: "http://institutionfrontend.westeurope.azurecontainer.io:4200",
    viewportWidth: 1920,
    viewportHeight: 1080,
    watchForFileChanges: false,
    defaultCommandTimeout: 5000,
    requestTimeout: 5000,
    setupNodeEvents(on, config) {
      // implement node event listeners here
    },
  },
});
```
