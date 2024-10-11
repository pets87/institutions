import { Component, OnInit, signal, ViewChild, ElementRef, Input, OnChanges } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";
import { PageBaseComponent, Constants, toDisplayDateTime } from "@rr/shared";
import { ClassifierDto, InstitutionService, ClassifierService, InstitutionDto, InstitutionReplicationDto, InstitutionReplicationService } from "@rr/services";
import { Location } from '@angular/common';
import { MatCheckboxChange } from "@angular/material/checkbox";

@Component({
  selector: 'rr-component-publish-institution',
  templateUrl: './component-publish-institution.html',
  styleUrls: [ './component-publish-institution.scss' ]
})

export class PublishInstitutionComponent extends PageBaseComponent implements OnInit, OnChanges {

  @Input() institutions?: InstitutionDto[];

  constructor(protected router: Router,
    private route: ActivatedRoute,
    protected translate: TranslateService,
    protected institutionService: InstitutionService,
    protected institutionReplicationService: InstitutionReplicationService,
    protected classifierService: ClassifierService,
    protected location: Location) {
    super();
    super.init(router, translate);
  }
  @ViewChild('planConfirmDialog') planConfirmDialog!: ElementRef<HTMLDialogElement>;

  readonly panelOpenState = signal(false);
  isMassActions = false;
  environments: ClassifierDto[] = [];
  systems: ClassifierDto[] = [];
  replications: InstitutionReplicationDto[] = [];

  plannedTime: Date | null = null;

  selectedInstitutions: InstitutionDto[] = [];

  tableData: any = [];
  displayedColumns: string[] = [];

  replicationHistory: any = [];
  historyDisplayedColumns: string[] = [ 'institution', 'system', 'env' ];


  ngOnInit(): void {
    this.classifierService.getAllClassifiers().subscribe(result => {
      this.environments = result.filter(x => x.group === Constants.CLASSIFIER_GROUP_REPLICAITON_ENV).sort((a: ClassifierDto, b: ClassifierDto) => {
        return a.id - b.id;
      });
      this.systems = result.filter(x => x.group === Constants.CLASSIFIER_GROUP_REPLICAITON_SYSTEM);
      this.displayedColumns = [ 'system', ...this.environments.map(x => x.name_translation_code) ];


      if (this.institutions != null) {
        this.isMassActions = true;
        this.selectedInstitutions = this.institutions;
        this.loadHistory();
      } else {
        this.route.paramMap.subscribe(params => {
          const id = params.get('id');
          if (Number(id) > 0) {
            this.loadInstitution(Number(id));
          }
        });
      }
    });   
  }

  ngOnChanges() {
    if (this.institutions != null) {
      this.isMassActions = true;
      this.selectedInstitutions = this.institutions;
      this.loadHistory();
    } 
  }

  loadHistory() {
    this.replications = this.getSortedReplications();
    const historyList = this.replicationHistory.slice(); //copy array into new, so table will be updated
    this.replicationHistory = [];
    for (const rep of this.replications) {

      const replications = this.replications.filter(x => x?.institution_id === rep?.institution_id && x?.system_classifier_id === rep?.system_classifier_id);
      
      const envs = [];
      for (const rep of replications) {
        envs.push({
          system: this.systems.find(x => x.id === rep?.system_classifier_id),
          env: this.environments.find(x => x.id === rep?.environment_classifier_id)?.name_translation_code,
          rep: rep
        });
      }

      let historyRow = historyList.find((x: { institution: { id: number | undefined; }; system: { id: number | undefined; }; }) => x?.institution?.id === rep?.institution_id && x?.system?.id === rep?.system_classifier_id);
      if (historyRow != null) {
        historyRow.environments = envs;
        continue;
      } else {
        historyRow = {};
      }

      historyRow.institution = this.selectedInstitutions.find(x => x.id === rep?.institution_id);
      historyRow.system = this.systems.find(x => x.id === rep?.system_classifier_id);
      historyRow.environments = envs;

      historyList.push(historyRow);
    }
    
    this.replicationHistory = historyList.reverse(); //newer first
  }

  getSortedReplications() {
    return this.selectedInstitutions
      .flatMap(x => x.replications)
      .filter((replication): replication is InstitutionReplicationDto => replication != null)
      .sort((a: InstitutionReplicationDto, b: InstitutionReplicationDto) => {
        return b.id - a.id;
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
      this.selectedInstitutions = [ result ];
      if (result.replications)
        this.replications = [ ...result.replications, ...this.replications ];
      this.loadHistory();
    });
  }


  isDisabled(system: ClassifierDto, environment: string) {
    if (this.isMassActions) //don't disable on mass actions
      return false;
    const replication = this.getReplication(system, environment);
    return replication?.published_date_time != null || replication?.planned_publish_date_time != null;
  }

  isDefaultChecked(system: ClassifierDto, environment: string) {
    if (this.isMassActions) //always can add new replications on mass actions
      return false;
    const replication = this.getReplication(system, environment);
    return replication != null;
  }
  isAllChecked(environment: string) {
    const env = this.environments.find(x => x.name_translation_code == environment);
    const firstInstitutionId = this.selectedInstitutions[ 0 ]?.id;
    const checked = this.replications.filter(x => x.environment_classifier_id == env?.id && x.institution_id === firstInstitutionId);
    
    return checked.length >= this.systems.length;    
  }

  checkAllReplication(environment: string, event: MatCheckboxChange) {
    const env = this.environments.find(x => x.name_translation_code == environment);
    const checkedAndPublished = this.replications.filter(x => x.environment_classifier_id == env?.id && x.published_date_time != null);

    if (event.checked && env != null) {
      //add all
      this.replications = this.replications.filter(x => x.environment_classifier_id != env?.id); //remove all current environment replications
      this.replications = [ ...checkedAndPublished, ...this.replications ]; //add back already published replications

      const checkedSystems = checkedAndPublished.map(x => x.system_classifier_id);

      for (const element of this.systems) {
        const system = element;

        if (checkedSystems.includes(system.id))
          continue; //skip if already added
        //add new
        this.addReplications(env, system);
      }
    } else {
      //remove all
      this.replications = this.replications.filter(x => x.environment_classifier_id != env?.id || x.published_date_time != null);
    }
  }
  addReplications(env: ClassifierDto, system: ClassifierDto) {
    if (this.selectedInstitutions.length > 0) {
      for (const institution of this.selectedInstitutions) {
        this.replications.push({
          id: 0,
          institution_id: institution.id!,
          environment_classifier_id: env.id,
          system_classifier_id: system.id
        });
      }
    } 
  }
  checkReplication(system: ClassifierDto, environment: string, event: MatCheckboxChange) {
    
    if (event.checked) {
      //add
      const env = this.environments.find(x => x.name_translation_code == environment);

      this.addReplications(env!, system);
    } else {
      const replication = this.getReplication(system, environment);
      if (replication) {
        //remove
        this.replications = this.replications.filter(x =>
          !(x.system_classifier_id == replication.system_classifier_id &&
            x.environment_classifier_id == replication.environment_classifier_id)
        );
      }
    }
  }

  getHistoryPublishedText(rep: InstitutionReplicationDto)
  {
    return toDisplayDateTime(rep.published_date_time);
  }
  getHistoryPlannedText(rep: InstitutionReplicationDto)
  {
    return toDisplayDateTime(rep.planned_publish_date_time);
  }

  getPublishedText(system: ClassifierDto, environment: string) {
    const replication = this.getReplication(system, environment);
    if (replication) {
      return toDisplayDateTime(replication.published_date_time);
    }

    return null;
  }
  getPlannedText(system: ClassifierDto, environment: string) {

    const replication = this.getReplication(system, environment);
    if (replication) {
      return toDisplayDateTime(replication.planned_publish_date_time);
    }

    return null;
  }

  getReplication(system: ClassifierDto, environment: string) {
    const env = this.environments.find(x => x.name_translation_code == environment);   
    const filtered = this.replications.filter(x => x.system_classifier_id == system.id && x.environment_classifier_id == env?.id);
    return this.getNewestReplication(filtered);
  }

  getNewestReplication(replications: InstitutionReplicationDto[] | undefined): InstitutionReplicationDto | null {
    if (replications == null || replications.length == 0) {
      return null;
    }
    const sorted = replications.sort((a: InstitutionReplicationDto, b: InstitutionReplicationDto) => {
      return b.id - a.id;
    });

    return sorted[ 0 ];
  }


  planPublish() {
    if (this.plannedTime == null) {
      this.translate.get("institutions.page-edit-institution.error.plannedtime").subscribe((translation: string) => {
        window.alert(translation);
      });
      return;
    }
    this.closeConfirmDialog();

    for (const replication of this.replications) {
      replication.planned_publish_date_time = this.plannedTime;
    }

    const newReplications = this.replications.filter(x => !(x.id > 0));
    const existingReplications = this.replications.filter(x => x.id > 0);

    this.institutionReplicationService.plan(newReplications).subscribe(result => {    
      this.updateReplications(existingReplications, result);
      this.plannedTime = null;
      this.loadHistory();
      this.translate.get("institutions.page-edit-institution.planned").subscribe((translation: string) => {
        window.alert(translation);
      });
    });

  }

  publish() {

    const newReplications = this.replications.filter(x => !(x.id > 0));
    const existingReplications = this.replications.filter(x => x.id > 0);

    for (const replication of newReplications) {
      if (replication.planned_publish_date_time != null) {
        replication.published_date_time = new Date();
      }
    }

    this.institutionReplicationService.publish(newReplications).subscribe(result => {
      this.updateReplications(existingReplications, result);
      this.loadHistory();
      this.translate.get("institutions.page-edit-institution.published").subscribe((translation: string) => {
        window.alert(translation);
      });
    });

  }

  updateReplications(existingReplications: InstitutionReplicationDto[], newReplications: InstitutionReplicationDto[] )
  {
    //update replications on form
    this.replications = [ ...existingReplications, ...newReplications ];

    //update replications on institutions
    for (let i = 0; i < this.selectedInstitutions.length; i++) {
      this.selectedInstitutions[ i ].replications = this.replications.filter(x => x.institution_id == this.selectedInstitutions[ i ].id);
    }
  }


  openConfirmDialog() {
    this.planConfirmDialog.nativeElement.show();
  }
  closeConfirmDialog() {
    this.planConfirmDialog.nativeElement.close();
  }

  back() {
    this.location.back();
  }

}
