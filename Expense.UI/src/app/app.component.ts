import { Component } from '@angular/core';
import { AuthServiceService } from './services/auth-service.service';
import { UserDetailsService } from './services/user-details.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'expense.ui';

  UserName : string = "";

  CheckStatus : boolean = false;

  public UserRole : string = "";

  constructor(private authenticateService : AuthServiceService, private userDetails : UserDetailsService, private router : Router
  ){}

  ngOnInit(): void{
    this.userDetails.GetStatus().subscribe(val => {
      if(val || this.authenticateService.IsLoggedIn()){
        this.CheckStatus = val || this.authenticateService.IsLoggedIn();
        if(this.CheckStatus){
          this.userDetails.GetUserRole().subscribe(val => {
            this.UserRole = val || this.authenticateService.getUserRoleDetail() || "" ;
          })
        }
        this.userDetails.GetUserName().subscribe(name => {
          let userName = this.authenticateService.getUserName();
          this.UserName = name || userName;
        })
      }
    })
  }

  LogOut(){
    this.UserName = "";
    this.CheckStatus = false;
    this.authenticateService.Logout();
  }

  CheckRole(){
    if(this.CheckStatus){
      if(this.UserRole == "Administrator"){
        return true;
      }else{
        return false;
      }
    }else{
      return false;
    }
  }

  CheckRole2(){
    if(this.CheckStatus){
      if(this.UserRole == "User"){
        return true;
      }else{
        return false;
      }
    }else{
      return true;
    }
  }

}
