describe('Trnslations tests', () => {


  it('Opens on default estonian', () => {
    cy.visit('/');
    cy.waitForAllRequests();
    
    cy.get('ul[id="nav"] a').last().should('contain.text', 'Asutused'); 
  })

  it('Opens on estonian', () => {
    cy.visit('/');
    cy.waitForAllRequests();

    cy.get('ul[id="nav"] a').last().should('contain.text', 'Asutused'); 
  })
  
  it('Opens on english', () => {
    cy.visit('/en');
    cy.waitForAllRequests();

    cy.get('ul[id="nav"] a').last().should('contain.text', 'Institutions'); 
  })

  it('Can change language', () => {
    cy.visit("/");    
    cy.waitForAllRequests();

    cy.get('#header > div > :nth-child(2)').last().click();
    cy.wait(100);
    cy.get('ul[id="nav"] a').last().should('contain.text', 'Institutions'); 
  })
  
  it('Can select tabs', () => {
    cy.visit("/");
    cy.waitForAllRequests();

    cy.contains('td', 'institutions.page-main.table.col.regcode').should('exist');
    cy.get('#mat-tab-label-0-0 > .mdc-tab__content > .mdc-tab__text-label').should('have.css', 'color', 'rgb(3, 169, 244)');
    cy.get('#mat-tab-label-0-1 > .mdc-tab__content > .mdc-tab__text-label').click();
    cy.wait(100);
    cy.get('#mat-tab-label-0-1 > .mdc-tab__content > .mdc-tab__text-label').should('have.css', 'color', 'rgb(3, 169, 244)');
    cy.contains('td', 'institution.4.name').should('exist');  

  })
  it('Can update translations', () => {
    cy.visit("/");

    cy.get('input[matinput]').first().clear().type('Changed').blur();
    cy.wait(100);
    cy.get("#top-message").should('exist');
    cy.wait(1500);
    cy.get("#top-message").should('not.exist'); //message should be shown for 1 second only

    //change back that
    cy.get('input[matinput]').first().clear().type('Reg Kood').blur();
    cy.wait(100);
    cy.get("#top-message").should('exist');
  }) 
  

})