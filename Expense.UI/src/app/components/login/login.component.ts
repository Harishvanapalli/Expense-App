import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthServiceService } from 'src/app/services/auth-service.service';
import { UserDetailsService } from 'src/app/services/user-details.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginForm! : FormGroup;

  constructor(private fb: FormBuilder, private AuthenticationService : AuthServiceService, private userDetails : UserDetailsService,
              private router : Router
  ){}

  ngOnInit() : void{
    this.loginForm = this.fb.group({
      username : ['', [Validators.required, Validators.email]],
      password : ['', Validators.required]
    })
  }

  onLogin(){
    if(this.loginForm.valid){
      this.AuthenticationService.CheckDetails(this.loginForm.value).subscribe({
        next : (res:any)=>{
            this.loginForm.reset();
            localStorage.setItem('token', res.token)
    
            this.userDetails.SetStatusToTrue();
            const payload = this.AuthenticationService.DecodeToken();
            this.userDetails.SetUserName(payload.unique_name)
      
            this.userDetails.SetUserRole(payload.role);
            this.router.navigate(['/'])
            window.alert('Login Success')
        }, error: (err)=>{
          if (err.status === 401) {
            window.alert("Unauthorized: Incorrect login details.");
          } else if (err?.error?.message) {
            window.alert(err.error.message);
          } else {
            window.alert("An unexpected error occurred.");
          }
        }
      })
      //console.log(this.loginForm.value)
    }
  }

}
