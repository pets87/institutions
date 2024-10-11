import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";
import { PageBaseComponent } from "@rr/shared";
import { TranslationService } from "@rr/services";

@Component({
  selector: 'rr-page-admin',
  templateUrl: './page-admin.component.html',
  styleUrls: [ './page-admin.component.scss' ]
})

export class PageAdminComponent extends PageBaseComponent implements OnInit {
  constructor(router: Router, translate: TranslateService, protected translationService: TranslationService) {
    super();
    super.init(router, translate);
  }

  translationTypes: string[] = [];


  ngOnInit(): void {
    this.initTranslationTypes();
  }

  initTranslationTypes() {
    this.translationService.getExistingTranslationTypes().subscribe(result => {
      this.translationTypes = result;
    });
  }
}
