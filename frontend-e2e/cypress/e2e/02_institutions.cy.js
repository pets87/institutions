describe('Institutions tests', () => {


  it('Institutions page loads', () => {
    cy.visit('/');
    cy.waitForAllRequests();

    cy.get('ul[id="nav"] a').last().click();
    cy.wait(1000);
    cy.get('span[class="mdc-button__label"]').should('exist');
    cy.get('tbody[class="mdc-data-table__content"]').should('exist');
  })

  it('Institutions page translation works', () => {
    cy.visit('/');
    
    cy.get('ul[id="nav"] a').last().click();

    cy.waitForAllRequests();
    
    cy.get('tbody[class="mdc-data-table__content"]').get('tr').contains('td', 'Saue vallavalitsus').should('exist');

    cy.get('#header > div > :nth-child(2)').last().click();
    cy.wait(100);

    cy.get('tbody[class="mdc-data-table__content"]').get('tr').contains('td', 'Saue municipal government').should('exist');

  })

  it('New institution page back navigation works', () => {
    cy.visit('/');

    cy.get('ul[id="nav"] a').last().click();

    cy.waitForAllRequests();

    cy.contains('span.mdc-button__label', 'Uus asutus').click();
    cy.wait(500);
    
    
    cy.contains('span.mdc-button__label', 'Tagasi').click();
    cy.wait(500);
    cy.get('tbody[class="mdc-data-table__content"]').should('exist');
  })


  it('Can create new institution', () => {
    cy.visit('/');

    cy.get('ul[id="nav"] a').last().click();

    cy.waitForAllRequests();

    cy.contains('span.mdc-button__label', 'Uus asutus').click();
    cy.wait(500);

    //Publish tab should be disabled when creating new institution
    cy.contains('span.mdc-tab__text-label', 'Publitseeri')
      .parents('div[role="tab"]')
      .should('have.attr', 'aria-disabled', 'true');

    createNewInstitution('123456', 'KMKR12345', 'Vallavalitsus');

    //Publish tab should be enabled after creating new institution
    cy.contains('span.mdc-tab__text-label', 'Publitseeri')
      .parents('div[role="tab"]')
      .should('not.have.attr', 'aria-disabled', 'true');


    //go back to first page
    cy.get('ul[id="nav"] a').last().click();
    cy.waitForAllRequests();

    //check if page has newly added row
    cy.get('tbody[class="mdc-data-table__content"]').get('tr').contains('td', 'Vallavalitsus').should('exist');

    //delete created row
    cy.get('tbody[class="mdc-data-table__content"]').get('tr').contains('td', 'Vallavalitsus').parents('tr.mat-mdc-row').within(() => {
      cy.get('mat-icon[fonticon="delete"]').click();
    });
    
  })



  it('Can create new institution and publish', () => {
    cy.visit('/');

    cy.get('ul[id="nav"] a').last().click();

    cy.waitForAllRequests();

    cy.contains('span.mdc-button__label', 'Uus asutus').click();
    cy.wait(500);

    //Publish tab should be disabled when creating new institution
    cy.contains('span.mdc-tab__text-label', 'Publitseeri')
      .parents('div[role="tab"]')
      .should('have.attr', 'aria-disabled', 'true');

    createNewInstitution('123456', 'KMKR12345', 'Vallavalitsus');    

    //Publish tab should be enabled after creating new institution
    cy.contains('span.mdc-tab__text-label', 'Publitseeri')
      .parents('div[role="tab"]')
      .should('not.have.attr', 'aria-disabled', 'true');

    //navigate to publish tab
    cy.contains('span.mdc-tab__text-label', 'Publitseeri').click();
    cy.wait(300);
    cy.get('tbody[class="mdc-data-table__content"]').first().get('tr').contains('td', 'Epood').should('exist');



    headerCheckboxesWork(1);
    headerCheckboxesWork(2);
    headerCheckboxesWork(3);
    headerCheckboxesWork(4);
    

    //check first row first checkbox
    cy.get('thead').get('tr').contains('td', 'Epood').parents('tr.mat-mdc-row').within(() => {
      cy.get('input.mdc-checkbox__native-control').first().click();
    });

    cy.contains('span.mdc-button__label', 'Publitseeri').click();
    cy.waitForAllRequests();

    cy.get('tbody[class="mdc-data-table__content"]').eq(1).within(() => {
      cy.get('tr').should('have.length', 1);

      cy.get('tr').filter((index, element) => {
        return Cypress.$(element).find('td').text().includes('Vallavalitsus (123456)');
      }).should('exist');      
    })


    //check first row second checkbox
    cy.get('thead').get('tr').contains('td', 'Epood').parents('tr.mat-mdc-row').within(() => {
      cy.get('input.mdc-checkbox__native-control').eq(1).click();
    });

    cy.get(':nth-child(2) > .mdc-button__label').click();
    cy.get('dialog').within(() => {
      cy.get('mat-datepicker-toggle').eq(0).within(() => {
        cy.get('span.mat-mdc-button-touch-target').click();    
        cy.wait(100);
      })     
    })
    cy.get('button.mat-calendar-body-active').click();//select current date
    cy.get('dialog').within(() => {
      cy.contains('span.mdc-button__label', 'Planeeri').click();
      cy.waitForAllRequests();
    })
   
    cy.get('tbody[class="mdc-data-table__content"]').eq(1).within(() => {
      cy.get('tr').should('have.length', 1);

      cy.get('tr').find('td').eq(2).within(() => {
        cy.get('input[type="checkbox"]').should('have.length', 2);
      })      
    })



    //go back to first page
    cy.get('ul[id="nav"] a').last().click();
    cy.waitForAllRequests();

    //check if page has newly added row
    cy.get('tbody[class="mdc-data-table__content"]').get('tr').contains('td', 'Vallavalitsus').should('exist');

    //delete created row
    cy.get('tbody[class="mdc-data-table__content"]').get('tr').contains('td', 'Vallavalitsus').parents('tr.mat-mdc-row').within(() => {
      cy.get('mat-icon[fonticon="delete"]').click();
    });
    cy.waitForAllRequests();

    //check if newly added row is deleted
    cy.get('tbody[class="mdc-data-table__content"]').get('tr').contains('td', 'Vallavalitsus').should('not.exist');

  })


  function headerCheckboxesWork(columnIndex) {
    // Click the header checkbox in the specified column
    cy.get('thead').first().within(() => {
      cy.get('tr').find('th').eq(columnIndex).within(() => {
        cy.get('input.mdc-checkbox__native-control').click();
      });
    });

    // Assert if all checkboxes in that column are checked
    cy.get('tbody[class="mdc-data-table__content"]').first().within(() => {
       cy.get('tr').each(($row, index) => {
         cy.wrap($row).find('td').eq(columnIndex).find('input[type="checkbox"]').then(($checkbox) => {
           cy.wrap($checkbox).should('be.checked');
         });
       });
    })
    
    // Uncheck the header checkbox in the specified column
    cy.get('thead').first().within(() => {
      cy.get('tr').find('th').eq(columnIndex).within(() => {
        cy.get('input.mdc-checkbox__native-control').click();
      });
    });
  }
    

  it('Can bulk publish', () => {
    cy.visit('/');

    //create first institution
    cy.get('ul[id="nav"] a').last().click();
    cy.waitForAllRequests();
    cy.contains('span.mdc-button__label', 'Uus asutus').click();
    cy.wait(500);
    createNewInstitution('54321', 'KMKR945940', 'Riigiasutus');    
    //go back to first page
    cy.get('ul[id="nav"] a').last().click();
    cy.waitForAllRequests();    
    

    //create second institution
    cy.get('ul[id="nav"] a').last().click();
    cy.waitForAllRequests();
    cy.contains('span.mdc-button__label', 'Uus asutus').click();
    cy.wait(500);
    createNewInstitution('75922', 'KMKR802093', 'Valitsus');
    //go back to first page
    cy.get('ul[id="nav"] a').last().click();
    cy.waitForAllRequests();   

    cy.contains('span.mdc-button__label', 'Masstegevused').click();
    cy.wait(100);

    //select created institutions
    cy.get('tbody[class="mdc-data-table__content"]').get('tr').contains('td', 'Riigiasutus').parents('tr.mat-mdc-row').within(() => {
      cy.get('input[type="checkbox"]').click();
    });
    cy.get('tbody[class="mdc-data-table__content"]').get('tr').contains('td', 'Valitsus').parents('tr.mat-mdc-row').within(() => {
      cy.get('input[type="checkbox"]').click();
    });

    cy.contains('span.mdc-button__label', 'Publitseeri').click();
    cy.wait(100);
    cy.get('dialog:visible > div#dialog-content').within(() => {
      //check first row first checkbox
      cy.get('thead').get('tr').contains('td', 'Epood').parents('tr.mat-mdc-row').within(() => {
        cy.get('input.mdc-checkbox__native-control').first().click();
      });

      cy.contains('span.mdc-button__label', 'Publitseeri').click();
      cy.waitForAllRequests();

      cy.get('tbody[class="mdc-data-table__content"]').eq(1).within(() => {
        cy.get('tr').should('have.length', 2);       
      })
    });

    cy.get('dialog:visible > div#dialog-header').click();


    //delete created institutions
    cy.get('tbody[class="mdc-data-table__content"]').get('tr').contains('td', 'Riigiasutus').parents('tr.mat-mdc-row').within(() => {
      cy.get('mat-icon[fonticon="delete"]').click();
    });
    cy.get('tbody[class="mdc-data-table__content"]').get('tr').contains('td', 'Valitsus').parents('tr.mat-mdc-row').within(() => {
      cy.get('mat-icon[fonticon="delete"]').click();
    });

  })
  
  

  function createNewInstitution(regCode, kmkr, name)
  {
    //fill form
    cy.get('select.mat-mdc-input-element').select('Riigiasutus');

    cy.contains('mat-label', 'Reg Kood ').closest('.mat-mdc-form-field-infix').find('input').type(regCode);
    cy.contains('mat-label', 'KMKR ').closest('.mat-mdc-form-field-infix').find('input').type(kmkr);
    cy.contains('mat-label', 'Aadress').closest('.mat-mdc-form-field-infix').find('input').type('Harju');
    
    cy.wait(500);
    cy.get('div.mat-mdc-autocomplete-panel').within(() => {
      cy.get('mat-option').first().within(() => {
        cy.get('.mdc-list-item__primary-text').click();
      })
    })   

    cy.get('mat-datepicker-toggle').eq(0).within(() => {
      cy.get('span.mat-mdc-button-touch-target').click();
    })

    cy.get('button.mat-calendar-body-active').click();//select current date

    cy.get('.cdk-overlay-backdrop').then(($el) => {
      $el.remove();
    });
    cy.wait(100);

    cy.contains('mat-label', 'Nimi ').closest('.mat-mdc-form-field-infix').find('input').type(name);  

    cy.get('.mat-expansion-indicator').click();
    cy.contains('mat-label', 'et ').closest('.mat-mdc-form-field-infix').find('input').type('Eesti ' + name);  
    cy.contains('mat-label', 'en ').closest('.mat-mdc-form-field-infix').find('input').type('English ' + name);  

    cy.contains('span.mdc-button__label', 'Salvesta').click();

    cy.waitForAllRequests();
  }











})