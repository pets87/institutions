describe('RabbitMq tests', () => {


  it.only('Institutions published to rabbit', () => {
    cy.visit('http://institutionsrabbitmq.westeurope.azurecontainer.io:15672');
    cy.wait(1500);
    cy.get('input[name="username"]').type('guest');
    cy.get('input[name="password"]').type('guest');
    cy.get('input[value="Login"]').click();
    cy.wait(1500);

    cy.get('#queues-and-streams > a').click();
    cy.wait(1000);

    cy.get('table.list > tbody > tr').should('have.length', 16);

    cy.get('table.list > tbody > tr').contains('td > a', 'Epood_DEV').parents('tr').within(() => {
      cy.get('td').eq(5).invoke('text')
        .then((text) => {
          const number = parseFloat(text);
          expect(number).to.be.greaterThan(0);
        });
    })
    
  })
})