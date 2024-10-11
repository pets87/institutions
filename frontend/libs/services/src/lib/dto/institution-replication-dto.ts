export interface InstitutionReplicationDto {
  id: number;
  institution_id: number;
  environment_classifier_id: number;
  system_classifier_id: number;
  planned_publish_date_time?: Date;
  published_date_time?: Date;  
}
