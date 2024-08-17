import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ExpenseTwoModel } from '../models/expensetwo';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class AdminActionsService {

  constructor(private http: HttpClient) { }

  AdminController : string = "AdminActions";

  public updateExpenseDetails(expense : ExpenseTwoModel) : Observable<any>{
    return this.http.put<any>(`${environment.apiURL}/${this.AdminController}/${'updateExpense'}`, expense);
  }

  public getAllUsers() : Observable<any>{
    return this.http.get<any>(`${environment.apiURL}/${this.AdminController}/${'getUsers'}`);
  }

  public CheckUserExistGroups(userId : string) : Observable<any>{
    return this.http.get<any>(`${environment.apiURL}/${this.AdminController}/${'checkUserExist'}/${userId}`);
  }

  public DeleteUser(userId : string) : Observable<any>{
    return this.http.delete<any>(`${environment.apiURL}/${this.AdminController}/${'deleteUser'}/${userId}`);
  }
}
