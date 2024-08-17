import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Expenses } from 'src/app/models/Expenses';
import { ExpenseTwoModel } from 'src/app/models/expensetwo';
import { AuthServiceService } from 'src/app/services/auth-service.service';
import { ExpenseServicesService } from 'src/app/services/expense-services.service';
import { UserDetailsService } from 'src/app/services/user-details.service';

@Component({
  selector: 'app-expenses-groups',
  templateUrl: './expenses-groups.component.html',
  styleUrls: ['./expenses-groups.component.css']
})
export class ExpensesGroupsComponent {
  constructor(private router : Router, private expenseService : ExpenseServicesService, private userDetails : UserDetailsService,
              private authService : AuthServiceService
  ){}

  public ExpenseGroups : Expenses[] = [];

  public UserName: string = "";

  ngOnInit(): void{
    this.userDetails.GetUserName().subscribe(val => {
      this.UserName = val || this.authService.getUserName();
    })
    this.expenseService.GetExpenseGroups(this.UserName).subscribe({
      next : (res:any)=> {
        if(res.isSuccess){
          this.ExpenseGroups = res.result;
        }
       // console.log(this.ExpenseGroups)
      }, error : (err) => {
        if(err?.error?.message){
          console.log(err.error.message);
        }
      }
    })
  }

  openExpenses(groupExpenses : any, groupId : number){
    this.router.navigate(['/groupExpenses'], { queryParams: { expenses : JSON.stringify(groupExpenses), groupID : groupId } });
  }
}
