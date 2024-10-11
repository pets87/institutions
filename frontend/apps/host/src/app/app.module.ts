import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { appRoutes } from './app.routes';
import { TopMessageComponent, materialImports, translationImports } from "@rr/shared";
import { PageAdminComponent } from './page-admin/page-admin.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { EditTranslationComponent } from './component-edit-translations/component-edit-translations.component';
@NgModule({
  declarations: [ AppComponent, PageAdminComponent, EditTranslationComponent ],
  imports: [
    BrowserModule,
    TopMessageComponent,
    BrowserAnimationsModule, //needed for mat-tabs
    RouterModule.forRoot(appRoutes, { initialNavigation: 'enabledBlocking' }),
    ...translationImports,
    ...materialImports
  ],
  providers: [
    MatDatepickerModule, //for mat-date-picker
    MatNativeDateModule //for mat-date-picker
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
