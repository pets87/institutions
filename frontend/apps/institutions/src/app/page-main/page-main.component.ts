import { Component, OnInit, ViewChild, ElementRef } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import { Router } from "@angular/router";
import { Constants, PageBaseComponent, toDisplayDate } from "@rr/shared";
import { InstitutionService, ClassifierService, ClassifierDto, AddressDto } from "@rr/services";
import { InstitutionDto } from "../../../../../libs/services/src";
import { MatCheckboxChange } from "@angular/material/checkbox";

@Component({
  selector: 'rr-page-main',
  templateUrl: './page-main.component.html',
  styleUrls: [ './page-main.component.scss' ]
})


export class PageMainComponent extends PageBaseComponent implements OnInit {
  constructor(protected router: Router, protected translate: TranslateService, protected institutionService: InstitutionService, private classifierService: ClassifierService) {
    super();
    super.init(router, translate);
  }
  isMassActionsVisible = false;
  displayedColumns: string[] = [ 'name', 'type_classifier_id', 'reg_code', 'kmkr', 'address_id', 'valid_from', 'valid_to', 'actions' ];
  dataSource: InstitutionDto[] = [];
  classifiers: ClassifierDto[] = [];
  selectedInstitutions: InstitutionDto[] = [];

  @ViewChild('publishDialog') publishDialog!: ElementRef<HTMLDialogElement>;

  ngOnInit(): void {
    this.loadClassifiers();
    this.institutionService.getAllInstitutions()
      .subscribe((data: InstitutionDto[]) => {
        this.dataSource = data;
      }, (error) => {
        console.error(error);
      });
  }

  newInstitution() {
    this.router.navigate([ super.getNavigateUrl('/new') ]);
  }
  loadClassifiers() {
    this.classifierService.getAllClassifiers().subscribe(result => {
      this.classifiers = result.filter(x => x.group === Constants.CLASSIFIER_GROUP_INSTITUTION_TYPE);
    });
  }
  
  onEdit(row: InstitutionDto) {

    if (row.id == null) {
      this.translate.get("institutions.page-edit-institution.notfound").subscribe((translation: string) => {
        window.alert(translation);
      });
      return;
    }

    this.institutionService.getInstitutionById(row.id).subscribe(result => {
      if (result == null) {
        this.translate.get("institutions.page-edit-institution.notfound").subscribe((translation: string) => {
          window.alert(translation);
        });
      }
      else {
        this.router.navigate([ super.getNavigateUrl('/edit/' + row.id) ]);
      }
    });   
  }

  formatTime(date:Date) {
    return toDisplayDate(date);
  }
  getTypeTranslation(classifier_id: number): string  {
    const clv = this.classifiers.find(x => x.id == classifier_id);
    if (clv == null)
      return "";
    return clv?.name_translation_code;
  }

  onDelete(row: InstitutionDto) {
    this.translate.get("form.confirm.delete").subscribe((translation: string) => {
      const confirmed = confirm(translation);
      if (!confirmed)
        return;
      this.institutionService.delete(row).subscribe(result => {
        if (result === true)
          this.dataSource = this.dataSource.filter(x => x.id != row.id);
      });
    });
  }

  checkInstitution(institution: InstitutionDto, event: MatCheckboxChange) {
    if (event.checked) {
      const newList = [ ...this.selectedInstitutions ]; //copy to new list for trigger changes
      newList.push(institution);
      this.selectedInstitutions = newList;
    } else {
      this.selectedInstitutions = this.selectedInstitutions.filter(x => x.id != institution.id);
    }
  }

  openMassAction() {
    this.isMassActionsVisible = true;
  }
  closeMassAction() {
    this.isMassActionsVisible = false;    
  }

  openPublishDialog() {
    this.publishDialog.nativeElement.show();
  }
  
  closePublishDialog() {
    this.publishDialog.nativeElement.close();
  }

  


}
