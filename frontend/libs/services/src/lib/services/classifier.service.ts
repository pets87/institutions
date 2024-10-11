import { Injectable } from "@angular/core";
import { ServiceBase } from "./service.base";
import { Observable, from, of } from "rxjs";
import { ClassifierDto } from "../dto/classifier-dto";

@Injectable({
  providedIn: 'root',
})
export class ClassifierService extends ServiceBase {

  constructor() {
    super();
  }

  classifierCache: ClassifierDto[] | null = null;


  getAllClassifiers(): Observable<ClassifierDto[]> {

    if (this.classifierCache) {
      return of(this.classifierCache);
    }

    const promise: Promise<ClassifierDto[]> = fetch(this.backendUrl + '/Classifier').then(response => {
      return response.json();
    });

    if (!this.classifierCache) {
      promise.then(data => {
        this.classifierCache = data;
      });
    }

    return from(promise);
  }



}
