import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ExpenseServicesService } from 'src/app/services/expense-services.service';

@Component({
  selector: 'app-add-expense',
  templateUrl: './add-expense.component.html',
  styleUrls: ['./add-expense.component.css']
})
export class AddExpenseComponent {

  private group_id: number = 0;

  constructor(private fb : FormBuilder, private router: Router, private expenseService : ExpenseServicesService,
             private route : ActivatedRoute
  ){}

  ExpenseForm! : FormGroup;

  ngOnInit() : void{
    this.route.queryParams.subscribe(params => {
      if(params['groupID']){
        this.group_id = (Number)(params['groupID']);
        //console.log('Hii', this.group_id, typeof(this.group_id))
      }
    })
    this.ExpenseForm = this.fb.group({
      groupid : this.group_id,
      description: ['', [Validators.required, Validators.minLength(15), Validators.maxLength(50)]],
      amount: ['', [Validators.required, Validators.pattern(/^\d+(\.\d{1,2})?$/), Validators.min(0)]],
      paid_by : ['', [Validators.required, Validators.email]],
      split_among: ['', [Validators.required]]
    })
  }

  onSubmitForm(){
    if(this.ExpenseForm.valid){
      const membersList = this.ExpenseForm.value.split_among.split(',').map((member: string) => member.trim());

      var paid_by_name = this.ExpenseForm.value.paid_by;

      if(!membersList.includes(paid_by_name)){
        membersList.push(paid_by_name);
      }

      this.ExpenseForm.patchValue({
        split_among : membersList
      })

      //console.log(this.ExpenseForm.value)

      this.expenseService.AddExpense(this.ExpenseForm.value).subscribe({
        next : (res : any)=>{
          console.log(res);
          this.ExpenseForm.reset();
          window.alert('Expense added Successfuly');
        }, error : (err)=> {
          if(err?.error?.message){
            console.log(err.error.message);
          }
        }
      })
    }
  }

}
