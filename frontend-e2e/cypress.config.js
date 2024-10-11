const { defineConfig } = require("cypress");

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
