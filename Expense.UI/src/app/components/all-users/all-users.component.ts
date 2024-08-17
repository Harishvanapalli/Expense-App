import { Component } from '@angular/core';
import { UserModel } from 'src/app/models/UsersModel';
import { AdminActionsService } from 'src/app/services/admin-actions.service';

@Component({
  selector: 'app-all-users',
  templateUrl: './all-users.component.html',
  styleUrls: ['./all-users.component.css']
})
export class AllUsersComponent {

  constructor(private adminServices : AdminActionsService){}

  public Users : UserModel[] = [];

  ngOnInit(): void{
    this.adminServices.getAllUsers().subscribe({
      next: (res: any)=>{
          if(res.isSuccess){
            this.Users = res.result;
          }else{
            console.log(res.message)
          }
      }, error : (err) => {
        if(err?.error?.message){
          console.log(err.error.message);
        }
      }
    })
  }
  deleteUser(user : UserModel){
    const confirmation = window.confirm("Are you sure to delete the user?");
    if(confirmation){
      this.adminServices.CheckUserExistGroups(user.username).subscribe({
        next : (res:any)=>{
          if(res.isSuccess){
            this.adminServices.DeleteUser(user.username).subscribe({
                next: (res:any)=>{
                  if(res.isSuccess){
                    window.alert(res.message);
                  }else{
                    window.alert(res.message)
                  }
                }, error : (err) => {
                  if(err?.error?.message){
                    window.alert(err.error.message);
                  }
                }
              })
          }else{
            window.alert(res.message)
          }
        }, error : (err)=>{
          if(err?.error?.message){
            window.alert(err.error.message);
          }
        }
      })
    }
  }
}
