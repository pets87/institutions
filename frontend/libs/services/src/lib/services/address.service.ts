import { Injectable } from "@angular/core";
import { ServiceBase } from "./service.base";
import { AddressDto } from "../dto/address-dto";
import { Observable, from } from "rxjs";

@Injectable({
  providedIn: 'root',
})

export class AddressService extends ServiceBase {

  constructor() {
    super();
  }

  searchAddress(text: string): Observable<AddressDto[]> {
    const promise: Promise<AddressDto[]> = fetch(this.backendUrl + '/Address?text='+text).then(response => {
      return response.json();
    });

    return from(promise);
  }
}
