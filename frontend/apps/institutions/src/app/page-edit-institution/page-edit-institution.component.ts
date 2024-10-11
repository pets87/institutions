import { Component, signal} from "@angular/core";
import {  ActivatedRoute, Router } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";
import { PageBaseComponent } from "@rr/shared";

@Component({
  selector: 'rr-page-edit-institution',
  templateUrl: './page-edit-institution.component.html',
  styleUrls: [ './page-edit-institution.component.scss' ]
})

export class PageEditInstitutionComponent extends PageBaseComponent {
  isPublishTabDisabled = true;
  constructor(protected router: Router, protected route: ActivatedRoute,
    protected translate: TranslateService) {
    super();
    super.init(router, translate);


    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (Number(id) > 0) {
        this.isPublishTabDisabled = false;
      }
    });

  }
  readonly panelOpenState = signal(false); 
  

}
