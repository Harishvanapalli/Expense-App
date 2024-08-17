import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ExpenseTwoModel } from 'src/app/models/expensetwo';
import { AdminActionsService } from 'src/app/services/admin-actions.service';
import { AuthServiceService } from 'src/app/services/auth-service.service';
import { ExpenseServicesService } from 'src/app/services/expense-services.service';
import { UserDetailsService } from 'src/app/services/user-details.service';

@Component({
  selector: 'app-edit-expenses',
  templateUrl: './edit-expenses.component.html',
  styleUrls: ['./edit-expenses.component.css']
})
export class EditExpensesComponent {

  constructor(private route : ActivatedRoute, private fb : FormBuilder, private authSerice : AuthServiceService,
              private UserDetails : UserDetailsService, private expenseService : ExpenseServicesService, private router: Router,
              private adminServices : AdminActionsService
  ){}

  private UserName : string = "";

  ExpenseForm! : FormGroup;

  public expense : ExpenseTwoModel = new ExpenseTwoModel();

  ngOnInit() : void{
    this.UserDetails.GetUserName().subscribe(name => {
      this.UserName = name || this.authSerice.getUserName();
    })
    this.route.queryParams.subscribe(params => {
      if(params['expense']){
        this.expense = JSON.parse(params['expense']);
      }
    })
    this.ExpenseForm = this.fb.group({
      description: [this.expense.description, [Validators.required, Validators.minLength(15), Validators.maxLength(50)]],
      amount: [this.expense.amount, [Validators.required, Validators.pattern(/^\d+(\.\d{1,2})?$/), Validators.min(0)]],
      paid_by : [this.expense.paid_by, [Validators.required, Validators.email]],
      split_among: [this.expense.split_among.join(','), [Validators.required]],
      paid_members : [this.expense.paid_members.join(','), []]
    });
  }

  onSubmitForm(){
    if(this.ExpenseForm.valid){
      const splitmembersList = this.ExpenseForm.value.split_among.split(',').map((member: string) => member.trim());

      if(!splitmembersList.includes(this.ExpenseForm.value.paid_by)){
        splitmembersList.push(this.ExpenseForm.value.paid_by);
      }

      this.ExpenseForm.patchValue({
        split_among : splitmembersList
      })

      const paidmembersValue = this.ExpenseForm.value.paid_members;

      const paidmembersList = paidmembersValue ? this.ExpenseForm.value.paid_members.split(',').map((member: string) => member.trim()) : [];

      if(!paidmembersList.includes(this.ExpenseForm.value.paid_by)){
        paidmembersList.push(this.ExpenseForm.value.paid_by);
      }

      this.ExpenseForm.patchValue({
        paid_members : paidmembersList
      })

      const ExpenseFormValue = this.ExpenseForm.value;

      this.expense.description = ExpenseFormValue.description;
      this.expense.amount = ExpenseFormValue.amount;
      this.expense.paid_by = ExpenseFormValue.paid_by;
      this.expense.split_among = ExpenseFormValue.split_among;
      this.expense.paid_members = ExpenseFormValue.paid_members;

     var check = 0;
     this.expense.split_among.forEach(member => {
      if(this.expense.paid_members.includes(member)){
        check = check + 1;
      }
     })
     if(check == this.expense.split_among.length){
      this.expense.issettled = true;
     }else{
      this.expense.issettled = false;
     }

     this.adminServices.updateExpenseDetails(this.expense).subscribe({
      next : (res:any) => {
        console.log(res);
        this.ExpenseForm.reset();
        window.alert('Expense Updated Successfully');
      }, error : (err)=> {
        if(err?.error?.message){
          console.log(err.error.message);
        }
      }
     })

     this.router.navigate(['/expenseGroups'])
    }
  }
}
