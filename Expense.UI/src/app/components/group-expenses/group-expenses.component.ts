import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ExpenseModel } from 'src/app/models/expense';
import { ExpenseTwoModel } from 'src/app/models/expensetwo';
import { AuthServiceService } from 'src/app/services/auth-service.service';
import { ExpenseServicesService } from 'src/app/services/expense-services.service';
import { UserDetailsService } from 'src/app/services/user-details.service';

@Component({
  selector: 'app-group-expenses',
  templateUrl: './group-expenses.component.html',
  styleUrls: ['./group-expenses.component.css']
})
export class GroupExpensesComponent {
  constructor(private route : ActivatedRoute, private router : Router, private authService : AuthServiceService,
              private userDetails : UserDetailsService, private expenseService : ExpenseServicesService
  ){}

  public Expenses : ExpenseTwoModel[] = [];

  private GroupID : number = 0;

  public UserName : string = "";

  public UserRole : string = "";
  public Status: boolean = false;

  ngOnInit() : void{
    this.userDetails.GetStatus().subscribe(val => {
      this.Status = val || this.authService.IsLoggedIn();
    })
    if(this.Status){
      this.userDetails.GetUserRole().subscribe(val => {
        this.UserRole = val || this.authService.getUserRoleDetail() || "" ;
      })
    }
    
    this.userDetails.GetUserName().subscribe(val => {
      this.UserName = val || this.authService.getUserName();
    })
    this.route.queryParams.subscribe(params => {
      if(params['expenses']){
        this.Expenses = JSON.parse(params['expenses']);
      }
      if(params['groupID']){
        this.GroupID = (Number)(params['groupID']);
      }
      // console.log(this.GroupID, typeof(this.GroupID));
      //console.log('hii',this.Expenses)
    })
  }

  AddExpense(){
    this.router.navigate(['/addExpense'], {queryParams : {groupID : this.GroupID}});
  }

  CheckStatus(expense : ExpenseTwoModel): boolean{
    var check = 0;
    expense.split_among.forEach(member => {
      if(expense.paid_members.includes(member)){
        check+=1;
      }
    });
    if(check == expense.split_among.length){
      return true;
    }else{
      return false;
    }
  }

  CheckPay(expense : ExpenseTwoModel) : boolean{
    var check = false;
    expense.split_among.forEach(member => {
      if(member == this.UserName){
        check = true;
      }
    });
    return check;
  }

  CheckPaidorNot(expense : ExpenseTwoModel) : boolean{
    var check = false;
    expense.paid_members.forEach(member=> {
      if(member == this.UserName){
        check = true;
      }
    })
    return check;
  }

  PayExpense(Expensee : ExpenseTwoModel){
    (Expensee.paid_members as string[]).push(this.UserName);
    if(this.CheckStatus(Expensee) == true){
      Expensee.issettled = true;
    }
    this.expenseService.paidUserUpdate(Expensee).subscribe({
      next : (res : any)=> {
        console.log(res);
      }, error : (err)=> {
        if(err?.error?.message){
          console.log(err.error.message);
        }
      }
    })
  }

  EditExpense(expense : ExpenseTwoModel){
    this.router.navigate(['/editExpense'], {queryParams : {expense : JSON.stringify(expense)}});
  }
}
