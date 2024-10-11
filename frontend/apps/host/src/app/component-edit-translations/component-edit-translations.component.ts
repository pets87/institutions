import { Component, OnInit, Input } from "@angular/core";
import { Router } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";
import { PageBaseComponent } from "@rr/shared";
import { AddressService, TranslationDto, TranslationService } from "@rr/services";

@Component({
  selector: 'rr-component-edit-translations',
  templateUrl: './component-edit-translations.component.html',
  styleUrls: [ './component-edit-translations.component.scss' ]
})

export class EditTranslationComponent extends PageBaseComponent implements OnInit {

  @Input() translationtype?: string;

  constructor(private router: Router, private translate: TranslateService, protected translationService: TranslationService, protected addressService: AddressService) {
    super();
    super.init(router, translate);
  }

  tempMessage = '';

  languages: string[] = [];
  translations: TranslationDto[] = [];

  datasource: any = [];
  displayedColumns: string[] = [];

  ngOnInit(): void {
    this.initLanguages();
    this.initTranslations();
  }
  initLanguages() {
    this.translationService.getExistingLanguages().subscribe(result => {
      this.languages = result;
      this.displayedColumns = [ 'code', ...result ];
    });
  }
  initTranslations() {
    this.translationService.getAllTranslations(true).subscribe(result => {
      this.translations = result.filter(x => x.type === this.translationtype);
      this.initDataSource();
    });
  }
  initDataSource() {


    const groupedByCode: Record<string, TranslationDto[]> = this.translations.reduce((group: any, translation: TranslationDto) => {
      if (!group[ translation.code ]) {
        group[ translation.code ] = [];
      }
      group[ translation.code ].push(translation);
      return group;
    }, {} as Record<string, TranslationDto[]>);

    this.datasource = Object.keys(groupedByCode).map(code => {
      const row: any = { code };
      groupedByCode[ code ].forEach(translation => {
        row[ translation.language ] = translation.text;
      });
      return row;
    });


  }

  updateTranslation(changedRow: any) {
    const translations = this.translations.filter(x => x.code === changedRow.code);

    for (const translation of translations) {
      const newText = changedRow[ translation.language ];
      translation.text = newText;
      this.translationService.updateTranslation(translation);
    }
    this.translate.get("form.label.translationsaved").subscribe((translation: string) => {
      this.setTempMessage(translation);
    });

    
  }

  setTempMessage(message: string) {
    this.tempMessage = message;
    window.setTimeout(() => {
      this.tempMessage = '';
    }, 1000);
  }

}


