import { Route } from '@angular/router';
import { LanguageGuard } from '@rr/shared';
import { PageAdminComponent } from './page-admin/page-admin.component';


export const appRoutes: Route[] = [
  {
    path: ':lang',
    canActivate: [ LanguageGuard ],
    children: [
      {
        path: 'institutions',
        loadChildren: () =>
          import('institutions/Module').then((m) => m.RemoteEntryModule),
        canActivate: [ LanguageGuard ]
      },
      {
        path: '',
        canActivate: [ LanguageGuard ],
        component: PageAdminComponent,
      },      
     
    ]
  },
  {
    path: '',
    redirectTo: 'et',
    pathMatch: 'full'
  },
  {
    path: '**',
    redirectTo: 'et'
  },
  
];
