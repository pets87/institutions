import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { getCurrentLanguage } from "@rr/shared";

@Component({
  selector: 'rr-root',
  templateUrl: './app.component.html',
  styleUrls: [ './app.component.scss' ],
})
export class AppComponent implements OnInit {
  title = 'host';
  selectedLanguage = 'et';
  constructor(private router: Router) { }

  ngOnInit(): void {
    this.selectedLanguage = getCurrentLanguage();
  }
  
  getRouterLinkFor(link: string) {
    return getCurrentLanguage() + "/" + link;
  }

  switchLanguage(lang: 'et' | 'en') {
    const currentUrlSegments = window.location.href.substring(window.location.origin.length + 1).split('/');
    this.selectedLanguage = lang;
    currentUrlSegments[ 0 ] = lang;
    const newUrl = currentUrlSegments.join('/');

    this.router.navigate([ newUrl ]);
  }

}
