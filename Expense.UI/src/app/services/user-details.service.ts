import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserDetailsService {

  private UserName$ = new BehaviorSubject<string>("");

  private Status$ = new BehaviorSubject<boolean>(false);

  private Role$ = new BehaviorSubject<string>("");

  constructor() { }

  public SetUserName(name : string){
    this.UserName$.next(name);
  }

  public GetUserName(){
    return this.UserName$.asObservable();
  }

  public SetUserRole(role : string){
    this.Role$.next(role);
  }

  public GetUserRole(){
    return this.Role$.asObservable();
  }

  public SetStatusToTrue(){
    this.Status$.next(true);
  }

  public SetStatusToFalse(){
    this.Status$.next(false)
  }

  public GetStatus(){
    return this.Status$.asObservable();
  }
}
