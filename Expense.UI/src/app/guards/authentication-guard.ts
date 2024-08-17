import { Injectable } from '@angular/core';
import { CanActivateFn , CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router} from '@angular/router';
import { Observable } from 'rxjs';
import { AuthServiceService } from '../services/auth-service.service';
import { UserDetailsService } from '../services/user-details.service';

@Injectable({
  providedIn: 'root'
})

export class AuthenticateGuard implements CanActivate{
  constructor(private userService : AuthServiceService, private userDetails : UserDetailsService, private route: Router){}
  canActivate() : boolean{
    var check = false;
    this.userDetails.GetStatus().subscribe(val => {
        check = val || this.userService.IsLoggedIn();
    })
    if(check){
      return true;
    }else{
      window.alert('Please Login')
      this.route.navigate(['/loginpage']);
      return false;
    }
  }
}

