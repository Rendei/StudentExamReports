import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) { }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    console.log(route);
    const expectedRoles = route.data.roles as Array<string>;
    
    if (expectedRoles.includes(this.authService.getUserRole())) {
      return true;
    } else {
      // Redirect to access denied page or login page
      this.router.navigate(['/access-denied']);
      return false;
    }
  }
  
}
