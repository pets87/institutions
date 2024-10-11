import { Injectable } from '@angular/core';
import { ServiceBase } from './service.base';
import { Observable, of, from } from 'rxjs';
import { InstitutionReplicationDto } from '../dto/institution-replication-dto';
@Injectable({
  providedIn: 'root',
})
export class InstitutionReplicationService extends ServiceBase {

  constructor() {
    super();
  }
 

  publish(replications: InstitutionReplicationDto[]): Observable<InstitutionReplicationDto[]> {
    
    const promise: Promise<InstitutionReplicationDto[]> = fetch(this.backendUrl + "/InstitutionReplications", {
      method: "POST",
      body: JSON.stringify(replications),
      headers: {
        'Content-Type': 'application/json'
      }
    }).then(response => {
      return response.json();
    });
    return from(promise);
  }


  plan(replications: InstitutionReplicationDto[]): Observable<InstitutionReplicationDto[]> {
    const promise: Promise<InstitutionReplicationDto[]> = fetch(this.backendUrl + "/InstitutionReplications", {
      method: "POST",
      body: JSON.stringify(replications),
      headers: {
        'Content-Type': 'application/json'
      }
    }).then(response => {
      return response.json();
    });
    return from(promise);
  }
}
