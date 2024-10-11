import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";


@Component({
  template: '',
})

export class PageBaseComponent  {


  protected init(router: Router, translate: TranslateService) {
    router.events.subscribe(() => {
      const lang = this.getUrlLanguage(router);
      if (lang) {
        translate.use(lang);
      }
    });
  }
  protected getCurrentLocation() {
    return window.location.href.substring(window.location.origin.length + 1);

  }
  //navigation should work on both - host and remote. so get current path and navigate from there.
  protected getNavigateUrl(path: string): string {
   return this.getCurrentLocation() + path;
  }
  private getUrlLanguage(router: Router): string | null {
    const url = router.url;
    const match = url.match(/^\/([a-z]{2})(\/.*)?$/);
    return match ? match[ 1 ] : null;
  }
}
