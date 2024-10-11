import { Component, OnInit, signal } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";
import { PageBaseComponent, Constants } from "@rr/shared";
import { ClassifierDto, InstitutionService, ClassifierService, TranslationService, InstitutionDto, TranslationDto, AddressDto, AddressService } from "@rr/services";
import { Location } from '@angular/common';
import { FormControl } from "@angular/forms";
import { Observable, of } from "rxjs";
import { map, startWith } from 'rxjs/operators';

@Component({
  selector: 'rr-component-edit-institution',
  templateUrl: './component-edit-institution.html',
  styleUrls: [ './component-edit-institution.scss' ]
})

export class EditInstitutionComponent extends PageBaseComponent implements OnInit {
  constructor(protected router: Router,
    private route: ActivatedRoute,
    protected translate: TranslateService,
    protected translationService: TranslationService,
    protected institutionService: InstitutionService,
    protected classifierService: ClassifierService,
    protected addressService: AddressService,
    protected location: Location) {
    super();
    super.init(router, translate);
  }
  myControl = new FormControl('');
  filteredOptions?: Observable<AddressDto[]>;
  selectedAddress: AddressDto | null | undefined = null;

  readonly panelOpenState = signal(false);
  institutionTypes: ClassifierDto[] = [];
  institution: InstitutionDto = {};
  institutionTranslations: TranslationDto[] = [];
  formIsValid = false;
  formErrors: string[] = [];



  ngOnInit(): void {
    this.loadLanguages();
    this.loadClassifiers();
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (Number(id) > 0) {
        this.loadInstitution(Number(id));
      }
    });

    this.myControl.valueChanges.pipe(
      startWith(''),
      map(value => this._filter(value || '')),
    ).subscribe(result => {
      this.filteredOptions = result;
    });
    
  }
  private _filter(value: string): Observable<AddressDto[]> {
    if (value && value.length > 3)
      return this.addressService.searchAddress(value);
    return of([]);
  }

  onAddressSelected(event: any): void {
    const selectedOption = event.option.value as AddressDto;
    this.selectedAddress = selectedOption;
    this.institution.address_id = selectedOption.id;
    this.institution.address = selectedOption;
  }

  loadLanguages() {
    this.translationService.getExistingLanguages().subscribe(result => {
      this.institutionTranslations = result.map(lang => ({
        id: 0,
        language: lang,
        code: '',
        type: Constants.TRANSLATION_TYPE_INSTITUTION,
        text: ''
      }));
    });
  }

  loadClassifiers() {
    this.classifierService.getAllClassifiers().subscribe(result => {
      this.institutionTypes = result.filter(x => x.group === Constants.CLASSIFIER_GROUP_INSTITUTION_TYPE);
    });
  }


  loadInstitution(id: number) {
    this.institutionService.getInstitutionById(id).subscribe(result => {
      if (result == null) {

        this.translate.get("institutions.page-edit-institution.notfound").subscribe((translation: string) => {
          window.alert(translation);
        });
        this.back();
        return;
      }
      this.formIsValid = true;
      this.institution = result;
      this.selectedAddress = result.address;
      if (result.translations) {
        const tranList: TranslationDto[] = [];
        for (const institutionTranslation of this.institutionTranslations) {
          const existingTranslation = result.translations.find(
            tran => tran.language === institutionTranslation.language
          );
          if (existingTranslation) {
            tranList.push(existingTranslation);
          } else {
            tranList.push(institutionTranslation);
          }
        }

        this.institutionTranslations = tranList;
      }
    });
  }
  
  isValid(property: string) {
    return this.formErrors.includes(property);
  }

  validateForm() {
    if (this.institution.reg_code == null)
      this.formErrors.push('institution.reg_code');
    else
      this.formErrors = this.formErrors.filter(x => x != 'institution.reg_code');
    if (this.institution.type_classifier_id == null)
      this.formErrors.push('institution.type_classifier_id');
    else
      this.formErrors = this.formErrors.filter(x => x != 'institution.type_classifier_id');
    if (this.institution.address_id == null)
      this.formErrors.push('institution.address_id');
    else
      this.formErrors = this.formErrors.filter(x => x != 'institution.address_id');
    if (this.institution.valid_from == null)
      this.formErrors.push('institution.valid_from');
    else
      this.formErrors = this.formErrors.filter(x => x != 'institution.valid_from');
    if (this.institution.name == null)
      this.formErrors.push('institution.name');
    else
      this.formErrors = this.formErrors.filter(x => x != 'institution.name');

    return this.formErrors.length === 0;
  }

  save() {
    if (!this.validateForm()) {
      this.translate.get("institutions.page-edit-institution.error.requiredfields").subscribe((translation: string) => {
        window.alert(translation);
      });
      return;
    }
    this.institution.translations = this.institutionTranslations;

    this.institutionService.save(this.institution).subscribe(saved => {
      this.institution = saved;
      this.translate.get("institutions.page-edit-institution.saved").subscribe((translation: string) => {
        window.alert(translation);        
        let loc = super.getCurrentLocation();
        if (loc.endsWith('new')) {
          loc = loc.substring(0, loc.lastIndexOf('/'));
          this.router.navigate([ loc + '/edit/' + this.institution.id ]);
        }       
      });
    });
  }

  publish() {
    console.log("published");
  }

  back() {
    this.location.back();
  }

}
