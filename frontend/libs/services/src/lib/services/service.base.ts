import { isDevMode } from "@angular/core";

export class ServiceBase {
  backendUrl: string = isDevMode() ? "http://localhost:7255" : "/api";

}
