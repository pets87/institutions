import { Injectable } from '@angular/core';
import { ServiceBase } from './service.base';
import { Observable, of, from } from 'rxjs';
import { InstitutionDto } from '../dto/institution-dto';
import { InstitutionReplicationDto } from '../dto/institution-replication-dto';
@Injectable({
  providedIn: 'root',
})
export class InstitutionService extends ServiceBase {

  constructor() {
    super();
  }
  db: InstitutionDto[] = [];
  replicationDb: InstitutionReplicationDto[] = [];
  getAllInstitutions(): Observable<InstitutionDto[]> {
    const promise: Promise<InstitutionDto[]> = fetch(this.backendUrl + '/Institution?offset=0&limit=10').then(response => {
      return response.json();
    });
    return from(promise);
  }

  getInstitutionById(id: number): Observable<InstitutionDto | null> {
    const promise: Promise<InstitutionDto> = fetch(this.backendUrl + '/Institution/'+id).then(response => {
      return response.json();
    });
    return from(promise);   
  }

  delete(institution: InstitutionDto): Observable<boolean>{
    const promise: Promise<boolean> = fetch(this.backendUrl + '/Institution/' + institution.id, {
      method: "DELETE",
      body: JSON.stringify(institution),
      headers: {
        'Content-Type': 'application/json'
      }
    }).then(response => {
      return response.json();
    });
    return from(promise);
  }

  save(institution: InstitutionDto): Observable<InstitutionDto> {
    const requestMethod = institution.id != null && institution.id > 0 ? "PUT" : "POST";
    const path = institution.id != null && institution.id > 0 ? '/Institution/' + institution.id : "/Institution";

    const promise: Promise<InstitutionDto> = fetch(this.backendUrl + path, {
      method: requestMethod,
      body: JSON.stringify(institution),
      headers: {
        'Content-Type': 'application/json'
      }
    }).then(response => {
      return response.json();
    });
    return from(promise);
  }
  
}
