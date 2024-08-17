import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { AddExpenseGroupComponent } from './components/add-expense-group/add-expense-group.component';
import { AddExpenseComponent } from './components/add-expense/add-expense.component';
import { ExpensesGroupsComponent } from './components/expenses-groups/expenses-groups.component';
import { GroupExpensesComponent } from './components/group-expenses/group-expenses.component';
import { AuthenticateGuard } from './guards/authentication-guard';
import { HomeComponentComponent } from './components/home-component/home-component.component';
import { EditExpensesComponent } from './components/edit-expenses/edit-expenses.component';
import { AllUsersComponent } from './components/all-users/all-users.component';

const routes: Routes = [
  {
    path: 'loginpage', component: LoginComponent
  },
  {
    path : 'addexpensegroup', component : AddExpenseGroupComponent, canActivate:[AuthenticateGuard]
  },
  {
    path: 'expenseGroups', component : ExpensesGroupsComponent, canActivate:[AuthenticateGuard]
  },
  {
    path:'groupExpenses', component:GroupExpensesComponent, canActivate:[AuthenticateGuard]
  },
  {
    path:'addExpense', component: AddExpenseComponent, canActivate:[AuthenticateGuard]
  },
  {
    path:'', component: HomeComponentComponent
  },
  {
    path: 'editExpense', component: EditExpensesComponent, canActivate:[AuthenticateGuard]
  },
  {
    path: 'allUsers', component: AllUsersComponent, canActivate:[AuthenticateGuard]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
