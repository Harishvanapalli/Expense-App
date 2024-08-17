import { Component } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthServiceService } from 'src/app/services/auth-service.service';
import { ExpenseServicesService } from 'src/app/services/expense-services.service';
import { UserDetailsService } from 'src/app/services/user-details.service';

@Component({
  selector: 'app-add-expense-group',
  templateUrl: './add-expense-group.component.html',
  styleUrls: ['./add-expense-group.component.css']
})
export class AddExpenseGroupComponent {


  constructor(private fb : FormBuilder, private router: Router, private expenseService : ExpenseServicesService,
               private userDetails : UserDetailsService, private authService : AuthServiceService
  ){}

  public UserName : string = "";

  expenseGroup! : FormGroup;

  ngOnInit() : void{
    this.userDetails.GetUserName().subscribe(val=>{
      this.UserName = val || this.authService.getUserName();
    })
    this.expenseGroup = this.fb.group({
      group_name: ['', [Validators.required, Validators.minLength(10), Validators.maxLength(50)]],
      description: ['', [Validators.required, Validators.minLength(15), Validators.maxLength(50)]],
      members: ['', [Validators.required]],
      expenses: ['', [Validators.required, Validators.pattern(/^\d+(\.\d{1,2})?$/), Validators.min(0)]]
    })
  }

  onSubmitForm(){
    if(this.expenseGroup.valid){
      const membersList = this.expenseGroup.value.members.split(',').map((member: string) => member.trim());

      if(!membersList.includes(this.UserName)){
        membersList.push(this.UserName);
      }

      this.expenseGroup.patchValue({
        members : membersList
      })

      this.expenseService.AddExpenseGroup(this.expenseGroup.value).subscribe({
        next : (res : any)=>{
          console.log(res);
          this.expenseGroup.reset();
          window.alert('Expense Group added Successfully');
        }, error : (err)=> {
          if(err?.error?.message){
            console.log(err.error.message);
          }
        }
      })
    }
  }

}


