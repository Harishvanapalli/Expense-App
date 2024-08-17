import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import { AddExpenseGroupComponent } from './components/add-expense-group/add-expense-group.component';
import { AddExpenseComponent } from './components/add-expense/add-expense.component';
import { ExpensesGroupsComponent } from './components/expenses-groups/expenses-groups.component';
import { GroupExpensesComponent } from './components/group-expenses/group-expenses.component';
import { AuthInterceptorService } from './services/auth-interceptor.service';
import { HomeComponentComponent } from './components/home-component/home-component.component';
import { EditExpensesComponent } from './components/edit-expenses/edit-expenses.component';
import { AllUsersComponent } from './components/all-users/all-users.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    AddExpenseGroupComponent,
    AddExpenseComponent,
    ExpensesGroupsComponent,
    GroupExpensesComponent,
    HomeComponentComponent,
    EditExpensesComponent,
    AllUsersComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [{provide : HTTP_INTERCEPTORS, useClass : AuthInterceptorService, multi : true}],
  bootstrap: [AppComponent]
})
export class AppModule { }
