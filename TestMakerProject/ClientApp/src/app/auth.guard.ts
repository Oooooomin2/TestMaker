import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';


@Injectable()
export class AuthGuard implements CanActivate {

  constructor(private router: Router) {

  }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean {
    if (localStorage.getItem('token') != null)
      return true;
    else {
      this.router.navigate(['/account/login']);
      return false;
    }
    
  }

}
