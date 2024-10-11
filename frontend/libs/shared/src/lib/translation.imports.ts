import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { HttpClientModule } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { TranslationService } from '@rr/services';

export const translationImports = [
  HttpClientModule,
  TranslateModule.forRoot({
    defaultLanguage: 'et',
    useDefaultLang: true,
    loader: {
      provide: TranslateLoader,
      useFactory: createTranslateLoader,
      deps: [ TranslationService ]
    }
  }),
];

export class BackendTranslateLoader implements TranslateLoader {
  constructor(private translationService: TranslationService) { }

  getTranslation(lang: string): Observable<{ [ key: string ]: string }> {

    return  this.translationService.getAllTranslations(true).pipe(
      map(translations => {
        const translationMap: { [ key: string ]: string } = {};
        translations.forEach(translation => {
          if (translation.language === lang)
            translationMap[ translation.code ] = translation.text;
        });
        return translationMap;
      })
    );
  }
}

export function createTranslateLoader(translationService: TranslationService) {
  return new BackendTranslateLoader(translationService);
}
