import { Injectable } from '@angular/core';
import { ServiceBase } from './service.base';
import { Observable, of, from, map } from 'rxjs';
import { TranslationDto } from '../dto/translation-dto';

@Injectable({
  providedIn: 'root',
})
export class TranslationService extends ServiceBase {

  constructor() {
    super();
  }

  translationCache: TranslationDto[] | null = null;


  getAllTranslations(noCache: boolean): Observable<TranslationDto[]> {

   

    //todo delete


    //const mockTranslations: TranslationDto[] = [
    //  { id: 1, type:'FRONTEND', language:'en', code: 'host.app.nav.institutions', text: 'Institutions' },
    //  { id: 2, type:'FRONTEND', language:'en', code: 'host.app.footer.email', text: 'abi@rahvastikuregister.ee' },
    //  { id: 3, type:'FRONTEND', language:'en', code: 'host.app.footer.rrinfo', text: 'Read about population register' },
    //  { id: 4, type:'FRONTEND', language:'en', code: 'host.app.footer.govinfo', text: 'Data of local governments' },
    //  { id: 5, type:'FRONTEND', language:'en', code: 'host.app.footer.terms', text: 'Terms of usage' },
    //  { id: 6, type:'FRONTEND', language:'en', code: 'host.page-admin.description', text: 'This admin page has been created as a demonstration. Select \'Institutions\' from the menu to see the sample work. The application has been built using the \'module federation\' approach, as the tender specified that a microfrontend approach is being used.' },

    //  { id: 7, type: 'FRONTEND',  language:'en', code: 'institutions.page-main.table.col.type', text: 'Type' },
    //  { id: 8, type: 'FRONTEND',  language:'en', code: 'institutions.page-main.table.col.name', text: 'Name' },
    //  { id: 9, type: 'FRONTEND',  language:'en', code: 'institutions.page-main.table.col.regcode', text: 'Reg Code' },
    //  { id: 10, type: 'FRONTEND', language:'en', code: 'institutions.page-main.table.col.kmkr', text: 'KMKR' },
    //  { id: 11, type: 'FRONTEND', language:'en', code: 'institutions.page-main.table.col.address', text: 'Address'},
    //  { id: 12, type: 'FRONTEND', language:'en', code: 'institutions.page-main.table.col.validfrom', text: 'Valid From' },
    //  { id: 12, type: 'FRONTEND', language:'en', code: 'institutions.page-main.table.col.validto', text: 'Valid To' },

    //  { id: 13, type: 'FRONTEND', language: 'et', code: 'host.app.nav.institutions', text: 'Asutused' },
    //  { id: 14, type: 'FRONTEND', language: 'et', code: 'host.app.footer.email', text: 'abi@rahvastikuregister.ee' },
    //  { id: 15, type: 'FRONTEND', language: 'et', code: 'host.app.footer.govinfo',text: 'Kohalike omavalitsuste andmed' },
    //  { id: 16, type: 'FRONTEND', language: 'et', code: 'host.app.footer.terms', text: 'Kasutustingimused' },
    //  { id: 17, type: 'FRONTEND', language: 'et', code: 'host.page-admin.description', text: 'See admin leht on loodud ainult näidisena. Vali menüüst \'Institutions\', et näha proovitööd. Rakendus on üles ehitatud kasutades \'module federation\' lähenemist, kuna hankes oli välja toodud, et kasutusel on mikrofrontend lähenemine.' },

    //  { id: 18, type: 'FRONTEND', language:  'et', code: 'institutions.page-main.table.col.type', text: 'Tüüp' },
    //  { id: 19, type: 'FRONTEND', language:  'et', code: 'institutions.page-main.table.col.name',  text: 'Nimi' },
    //  { id: 20, type: 'FRONTEND', language:  'et', code: 'institutions.page-main.table.col.regcode', text: 'Reg Kood' },
    //  { id: 21, type: 'FRONTEND', language: 'et', code: 'institutions.page-main.table.col.kmkr',  text: 'KMKR' },
    //  { id: 22, type: 'FRONTEND', language: 'et', code: 'institutions.page-main.table.col.address', text: 'Aadress' },
    //  { id: 23, type: 'FRONTEND', language: 'et', code: 'institutions.page-main.table.col.validfrom',  text: 'Kehtiv alates' },
    //  { id: 24, type: 'FRONTEND', language: 'et', code: 'institutions.page-main.table.col.validto', text: 'Kehtiv Kuni' },
    //  { id: 25, type: 'INSTITUTION', language: 'et', code: 'institution.4.name', text: 'Linnavalitsus' },
    //  { id: 26, type: 'INSTITUTION', language: 'en', code: 'institution.4.name', text: 'City gov' },
    //  { id: 27, type: 'FRONTEND', language: 'en', code: 'institutions.page-main.table.col.actions', text: 'The actions' },
    //  { id: 28, type: 'FRONTEND', language: 'et', code: 'institutions.page-main.table.col.actions', text: 'Tegevused' },
    //  { id: 29, type: 'FRONTEND', language: 'et', code: 'form.button.back', text: 'Tagasi' },
    //  { id: 30, type: 'FRONTEND', language: 'en', code: 'form.button.back', text: 'Back' },
    //  { id: 31, type: 'FRONTEND', language: 'et', code: 'form.button.save', text: 'Salvesta' },
    //  { id: 32, type: 'FRONTEND', language: 'en', code: 'form.button.save', text: 'Save' },

    //  { id: 33, type: 'CLASSIFIER', language: 'et', code: 'classifier.1.name', text: 'Osaühing' },
    //  { id: 34, type: 'CLASSIFIER', language: 'en', code: 'classifier.1.name', text: 'Limited company' },
    //  { id: 35, type: 'CLASSIFIER', language: 'et', code: 'classifier.2.name', text: 'Aktsiaselts' },
    //  { id: 36, type: 'CLASSIFIER', language: 'en', code: 'classifier.2.name', text: 'Stock company' },
    //  { id: 37, type: 'CLASSIFIER', language: 'et', code: 'classifier.3.name', text: 'Riigiasutus' },
    //  { id: 38, type: 'CLASSIFIER', language: 'en', code: 'classifier.3.name', text: 'State institution' },
    //  { id: 39, type: 'FRONTEND', language: 'et', code: 'form.label.translations', text: 'Tõlked' },
    //  { id: 40, type: 'FRONTEND', language: 'en', code: 'form.label.translations', text: 'Translations' },
    //  { id: 41, type: 'FRONTEND', language: 'et', code: 'institutions.page-edit-institution.translations.description', text: 'Riigiasutused on tõlgitavad' },
    //  { id: 42, type: 'FRONTEND', language: 'en', code: 'institutions.page-edit-institution.translations.description', text: 'State institutions are translatable' },
    //  { id: 43, type: 'FRONTEND', language: 'et', code: 'form.date.format', text: 'pp/kk/aaaa' },
    //  { id: 44, type: 'FRONTEND', language: 'en', code: 'form.date.format', text: 'mm/dd/yyyy' },

    //  { id: 45, type: 'FRONTEND', language: 'et', code: 'institutions.page-edit-institution.error.reg_code', text: 'Registrikood on kohustuslik' },
    //  { id: 46, type: 'FRONTEND', language: 'en', code: 'institutions.page-edit-institution.error.reg_code', text: 'RegCode is ´mandatory' },
    //  { id: 47, type: 'FRONTEND', language: 'et', code: 'institutions.page-edit-institution.error.type_classifier_id', text: 'Tüüp on kohustuslik' },
    //  { id: 48, type: 'FRONTEND', language: 'en', code: 'institutions.page-edit-institution.error.type_classifier_id', text: 'Type is mandatory' },
    //  { id: 49, type: 'FRONTEND', language: 'et', code: 'institutions.page-edit-institution.error.address_id', text: 'Aadress on kohustuslik' },
    //  { id: 50, type: 'FRONTEND', language: 'en', code: 'institutions.page-edit-institution.error.address_id', text: 'Address is mandatory' },
    //  { id: 51, type: 'FRONTEND', language: 'et', code: 'institutions.page-edit-institution.error.valid_from', text: 'Kehtiv alates on kohustuslik' },
    //  { id: 52, type: 'FRONTEND', language: 'en', code: 'institutions.page-edit-institution.error.valid_from', text: 'Valid from is mandatory' },
    //  { id: 53, type: 'FRONTEND', language: 'et', code: 'institutions.page-edit-institution.error.name', text: 'Nimi on kohustuslik' },
    //  { id: 54, type: 'FRONTEND', language: 'en', code: 'institutions.page-edit-institution.error.name', text: 'Name is mandatory' },
    //  { id: 55, type: 'FRONTEND', language: 'et', code: 'institutions.page-edit-institution.error.requiredfields', text: 'Täida kohustuslikud väljad' },
    //  { id: 56, type: 'FRONTEND', language: 'en', code: 'institutions.page-edit-institution.error.requiredfields', text: 'Fill requiured fields' },
    //  { id: 55, type: 'FRONTEND', language: 'et', code: 'institutions.page-edit-institution.notfound', text: 'Asutust ei leitud' },
    //  { id: 56, type: 'FRONTEND', language: 'en', code: 'institutions.page-edit-institution.notfound', text: 'Institution not found' },
    //  { id: 57, type: 'FRONTEND', language: 'et', code: 'institutions.page-edit-institution.saved', text: 'Asutust salvestatud' },
    //  { id: 58, type: 'FRONTEND', language: 'en', code: 'institutions.page-edit-institution.saved', text: 'Institution saved' },
    //  { id: 59, type: 'FRONTEND', language: 'et', code: 'institutions.page-edit-institution.publish', text: 'Publitseeri' },
    //  { id: 60, type: 'FRONTEND', language: 'en', code: 'institutions.page-edit-institution.publish', text: 'Publish' },
    //  { id: 61, type: 'FRONTEND', language: 'et', code: 'institutions.page-edit-institution.tab.institution', text: 'Asutus' },
    //  { id: 62, type: 'FRONTEND', language: 'en', code: 'institutions.page-edit-institution.tab.institution', text: 'Institution' },
    //  { id: 63, type: 'FRONTEND', language: 'et', code: 'institutions.page-edit-institution.tab.publish', text: 'Publitseeri' },
    //  { id: 64, type: 'FRONTEND', language: 'en', code: 'institutions.page-edit-institution.tab.publish', text: 'Publish' },

    //  { id: 65, type: 'CLASSIFIER', language: 'et', code: 'classifier.4.name', text: 'Arendus' },
    //  { id: 66, type: 'CLASSIFIER', language: 'en', code: 'classifier.4.name', text: 'Development' },
    //  { id: 67, type: 'CLASSIFIER', language: 'et', code: 'classifier.5.name', text: 'Test' },
    //  { id: 68, type: 'CLASSIFIER', language: 'en', code: 'classifier.5.name', text: 'Test' },
    //  { id: 69, type: 'CLASSIFIER', language: 'et', code: 'classifier.6.name', text: 'Toodang' },
    //  { id: 70, type: 'CLASSIFIER', language: 'en', code: 'classifier.6.name', text: 'Production' },

    //  { id: 71, type: 'CLASSIFIER', language: 'et', code: 'classifier.7.name', text: 'Kinnisvara portaal' },
    //  { id: 72, type: 'CLASSIFIER', language: 'en', code: 'classifier.7.name', text: 'Real estate portal' },
    //  { id: 73, type: 'CLASSIFIER', language: 'et', code: 'classifier.8.name', text: 'Auto portaal' },
    //  { id: 74, type: 'CLASSIFIER', language: 'en', code: 'classifier.8.name', text: 'Car portal' },
    //  { id: 75, type: 'CLASSIFIER', language: 'et', code: 'classifier.9.name', text: 'Tööportaal' },
    //  { id: 76, type: 'CLASSIFIER', language: 'en', code: 'classifier.9.name', text: 'Work portal' },
    //  { id: 77, type: 'CLASSIFIER', language: 'et', code: 'classifier.10.name', text: 'Epood' },
    //  { id: 78, type: 'CLASSIFIER', language: 'en', code: 'classifier.10.name', text: 'Store' },

    //  { id: 79, type: 'FRONTEND', language: 'et', code: 'form.label.system', text: 'Süsteem' },
    //  { id: 80, type: 'FRONTEND', language: 'en', code: 'form.label.system', text: 'System' },
    //  { id: 81, type: 'FRONTEND', language: 'et', code: 'form.button.plan', text: 'Planeeri' },
    //  { id: 82, type: 'FRONTEND', language: 'en', code: 'form.button.plan', text: 'Plan' },

    //  { id: 83, type: 'FRONTEND', language: 'et', code: 'institutions.page-edit-institution.published', text: 'Asutused publitseeritud' },
    //  { id: 84, type: 'FRONTEND', language: 'en', code: 'institutions.page-edit-institution.published', text: 'Institutions published' },
    //  { id: 85, type: 'FRONTEND', language: 'et', code: 'institutions.page-edit-institution.planned', text: 'Asutuste publitseerimine planeeritud' },
    //  { id: 86, type: 'FRONTEND', language: 'en', code: 'institutions.page-edit-institution.planned', text: 'Institutions publishing planned' },

    //  { id: 87, type: 'FRONTEND', language: 'et', code: 'form.label.published', text: 'Publitseeritud' },
    //  { id: 88, type: 'FRONTEND', language: 'en', code: 'form.label.published', text: 'Published' },
    //  { id: 89, type: 'FRONTEND', language: 'et', code: 'form.label.planned', text: 'Planeeritud' },
    //  { id: 90, type: 'FRONTEND', language: 'en', code: 'form.label.planned', text: 'Planned' },
    //  { id: 91, type: 'FRONTEND', language: 'et', code: 'form.label.plannedtime', text: 'Planeeritud aeg' },
    //  { id: 92, type: 'FRONTEND', language: 'en', code: 'form.label.plannedtime', text: 'Planned time' },
    //  { id: 93, type: 'FRONTEND', language: 'et', code: 'institutions.page-edit-institution.planningtext', text: 'Vali aeg, millal planeerida valitud kirjete publitseerimine. Valitud kuupäeval publitseeritakse kirjed automaatselt. Planeeritud kirjeid on võimalik muuta. Juba publitseeritud kirjeid ei ole võimalik muuta.' },
    //  { id: 94, type: 'FRONTEND', language: 'en', code: 'institutions.page-edit-institution.planningtext', text: 'Select the time to schedule the publication of the chosen entries. The entries will be published automatically on the selected date. It is possible to edit scheduled entries. Already published entries cannot be edited.' },
    //  { id: 95, type: 'FRONTEND', language: 'et', code: 'institutions.page-edit-institution.error.plannedtime', text: 'Planeeritud aeg on kohustuslik' },
    //  { id: 96, type: 'FRONTEND', language: 'en', code: 'institutions.page-edit-institution.error.plannedtime', text: 'Planned time is mandatory' },
    //  { id: 97, type: 'FRONTEND', language: 'et', code: 'institutions.page-main.button.newinstitution', text: 'Uus asutus' },
    //  { id: 98, type: 'FRONTEND', language: 'en', code: 'institutions.page-main.button.newinstitution', text: 'New institution' },
    //  { id: 99, type: 'FRONTEND', language: 'et', code: 'form.confirm.delete', text: 'Kas oled kindel, et soovid kirje kustutada?' },
    //  { id: 100, type: 'FRONTEND', language: 'en', code: 'form.confirm.delete', text: 'Are you sure you want to delete this row?' },
    //  { id: 101, type: 'FRONTEND', language: 'et', code: 'institutions.page-main.button.massactions', text: 'Masstegevused' },
    //  { id: 102, type: 'FRONTEND', language: 'en', code: 'institutions.page-main.button.massactions', text: 'Mass actions' },
    //  { id: 103, type: 'FRONTEND', language: 'et', code: 'form.button.cancel', text: 'Tühista' },
    //  { id: 104, type: 'FRONTEND', language: 'en', code: 'form.button.cancel', text: 'Cancel' },
    //  { id: 105, type: 'FRONTEND', language: 'et', code: 'institutions.page-edit-institution.selectedinstitutions', text: 'Valitud asutused' },
    //  { id: 106, type: 'FRONTEND', language: 'en', code: 'institutions.page-edit-institution.selectedinstitutions', text: 'Selected institutions' },
    //  { id: 107, type: 'FRONTEND', language: 'et', code: 'form.label.environment', text: 'Keskkond' },
    //  { id: 108, type: 'FRONTEND', language: 'en', code: 'form.label.environment', text: 'Environment' },
    //  { id: 109, type: 'FRONTEND', language: 'et', code: 'institutions.page-edit-institution.history', text: 'Publitseerimise Ajalugu' },
    //  { id: 110, type: 'FRONTEND', language: 'en', code: 'institutions.page-edit-institution.history', text: 'Publish history' },
    //  { id: 111, type: 'FRONTEND', language: 'et', code: 'form.label.translationcode', text: 'Tõlke kood' },
    //  { id: 112, type: 'FRONTEND', language: 'en', code: 'form.label.translationcode', text: 'Translation code' },
    //  { id: 113, type: 'FRONTEND', language: 'et', code: 'host.app.footer.rrinfo', text: 'Loe rahvastikuregistri kohta' },
    //  { id: 114, type: 'FRONTEND', language: 'et', code: 'form.label.translationsaved', text: 'Tõlge salvestatud' },
    //  { id: 115, type: 'FRONTEND', language: 'en', code: 'form.label.translationsaved', text: 'Translation saved' },
    //  { id: 116, type: 'FRONTEND', language: 'et', code: 'component-edit-translations.changetext', text: 'Muutmisel toimub automaatne salvestamine' },
    //  { id: 117, type: 'FRONTEND', language: 'en', code: 'component-edit-translations.changetext', text: 'Changes are saved automatically.' },
    //  { id: 118, type: 'FRONTEND', language: 'et', code: 'component-edit-translations.infotext', text: 'Uute tõlgete lisamist ei ole loodud, kuna uue tõlke kasutusele võtmine vajab ka koodimuudatust' },
    //  { id: 119, type: 'FRONTEND', language: 'en', code: 'component-edit-translations.infotext', text: 'Adding new translations has not been implemented because adopting a new translation also requires a code change.' },

    //];

    //if (!this.translationCache) {
    //  this.translationCache = mockTranslations;
    //}

    //return of(mockTranslations);



    if (this.translationCache && !noCache) {
      return of(this.translationCache);
    }

    const promise: Promise<TranslationDto[]> = fetch(this.backendUrl + '/Translation').then(response => {
      return response.json();
    });

    if (!this.translationCache) {
      promise.then(data => {
        this.translationCache = data;
      });
    }

    return from(promise);
  }

  getExistingLanguages(): Observable<string[]> {
    return this.getAllTranslations(false).pipe(
      map(result => {
        return Array.from(new Set(result.map(item => item.language)));
      })
    );
  }
  getExistingTranslationTypes(): Observable<string[]> {
    return this.getAllTranslations(false).pipe(
      map(result => {
        return Array.from(new Set(result.map(item => item.type)));
      })
    );
  }
  updateTranslation(translation: TranslationDto): Observable<TranslationDto> {

    const promise: Promise<TranslationDto> = fetch(this.backendUrl + '/Translation/' + translation.id, {
      method: "PUT",
      body: JSON.stringify(translation),
      headers: {
        'Content-Type': 'application/json'
      }
    }).then(response => {
      return response.json();
    });

    this.translationCache = null; //clear cache
    return from(promise);

    //TODO delete
    //if (this.translationCache != null) {
    //  const index = this.translationCache.findIndex(x => x.id === translation.id);
    //  if (index !== -1) {
    //    this.translationCache[ index ] = translation;
    //  }       
    //}
    //return of(translation);
  }
}


