import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router } from '@angular/router';


@Injectable({
  providedIn: 'root'
})
export class LanguageGuard implements CanActivate {

  constructor(private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot): boolean {
    let lang = route.params[ 'lang' ];
    if (lang == null)
      lang = route.url[ 0 ]?.path;
      
    if (lang === 'en' || lang === 'et') {
      return true;
    }
    
    this.router.navigate([ 'et']);
    return false;
  }
}


