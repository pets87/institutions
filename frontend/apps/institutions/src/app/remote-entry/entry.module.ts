import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { RemoteEntryComponent } from './entry.component';
import { remoteRoutes } from './entry.routes';
import { PageMainComponent } from '../page-main/page-main.component';
import { HttpClientModule } from '@angular/common/http';
import { materialImports, translationImports } from "@rr/shared";
import { PageEditInstitutionComponent } from '../page-edit-institution/page-edit-institution.component';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { EditInstitutionComponent } from '../component-edit-institution/component-edit-institution';
import { PublishInstitutionComponent } from '../component-publish-institution/component-publish-institution';

@NgModule({
  declarations: [ RemoteEntryComponent, PageMainComponent, PageEditInstitutionComponent, EditInstitutionComponent, PublishInstitutionComponent ],
  imports: [
    CommonModule,    
    HttpClientModule, 
    RouterModule.forChild(remoteRoutes),
    ...translationImports,
    ...materialImports
  ],
  providers: [
    MatDatepickerModule, //for mat-date-picker
    MatNativeDateModule //for mat-date-picker
  ],
  exports: [ ]
})
export class RemoteEntryModule {}
