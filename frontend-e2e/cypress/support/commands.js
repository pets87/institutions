Cypress.Commands.add('waitForAllRequests', () => {

  cy.intercept({ method: 'GET', url: '**' }).as('allRequests');
  cy.intercept({ method: 'POST', url: '**' }).as('allRequests');
  cy.intercept({ method: 'PUT', url: '**' }).as('allRequests');
  cy.intercept({ method: 'PATCH', url: '**' }).as('allRequests');
  cy.intercept({ method: 'DELETE', url: '**' }).as('allRequests');
  
  cy.wait('@allRequests', { timeout: 10000 });
});