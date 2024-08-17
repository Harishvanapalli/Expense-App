import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { User } from '../models/user';
import { JwtHelperService } from '@auth0/angular-jwt';
import { UserDetailsService } from './user-details.service';

@Injectable({
  providedIn: 'root'
})
export class AuthServiceService {

  private URl = "Authentication";

  private userpayload : any;

  constructor(private http: HttpClient, private router : Router, private userDetails : UserDetailsService) {
    this.userpayload= this.DecodeToken()
   }


  public CheckDetails(user : User) : Observable<any>{
    return this.http.post<any>(`${environment.apiURL}/${this.URl}/${'authenticate'}`, user);
  }

  getToken(){
    return localStorage.getItem('token')
  }

  IsLoggedIn() : boolean{
    return !!localStorage.getItem('token')
  }

  Logout(){
    localStorage.clear()
    this.userDetails.SetStatusToFalse()
    this.userDetails.SetUserRole('');
    this.router.navigate(['/'])
  }

  DecodeToken(){
    const token = this.getToken()!;
    const jwtHelper = new JwtHelperService()
    return jwtHelper.decodeToken(token)
  }

  getUserName(){
    if(this.userpayload){
      return this.userpayload.unique_name;
    }
  }

  getUserRoleDetail(){
    return this.userpayload?.role || null;
  }
}
