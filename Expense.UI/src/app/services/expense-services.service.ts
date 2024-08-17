import { HttpClient } from '@angular/common/http';
import { EmbeddedViewRef, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { ExpenseGroupModel } from '../models/expenseGroup';
import { ExpenseModel } from '../models/expense';
import { ExpenseTwoModel } from '../models/expensetwo';

@Injectable({
  providedIn: 'root'
})
export class ExpenseServicesService {

   ControllerName : string = "Expense";

  constructor(private http: HttpClient, private router: Router) { }

  public AddExpenseGroup(expenseGroup : ExpenseGroupModel) : Observable<any> {
    return this.http.post<any>(`${environment.apiURL}/${this.ControllerName}/${'addExpenseGroup'}`, expenseGroup);
  }

  public AddExpense(expense : ExpenseModel) : Observable<any>{
    return this.http.post<any>(`${environment.apiURL}/${this.ControllerName}/${'addExpense'}`, expense);
  }

  public GetExpenseGroups(userName : string) : Observable<any>{
    return this.http.get<any>(`${environment.apiURL}/${this.ControllerName}/${'getExpenseGroups'}/${userName}`);
  }

  public paidUserUpdate(expense : ExpenseTwoModel) : Observable<any>{
    return this.http.put<any>(`${environment.apiURL}/${this.ControllerName}/${'updateUserPaid'}`, expense);
  }
}
