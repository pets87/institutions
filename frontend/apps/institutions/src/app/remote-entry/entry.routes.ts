import { Route } from '@angular/router';
import { PageMainComponent } from '../page-main/page-main.component';
import { PageEditInstitutionComponent } from '../page-edit-institution/page-edit-institution.component';

export const remoteRoutes: Route[] = [
  { path: '', component: PageMainComponent },
  { path: 'new', component: PageEditInstitutionComponent },
  { path: 'edit/:id', component: PageEditInstitutionComponent },
];
