import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthServiceService } from './auth-service.service';

@Injectable()

export class AuthInterceptorService implements HttpInterceptor{

  constructor(private authService : AuthServiceService){}
  
  intercept(req: HttpRequest<any>, next: HttpHandler) {
    const token = this.authService.getToken();
    if(token){
      req = req.clone({
        setHeaders : {Authorization : `Bearer ${token}`}
      })
    }
    return next.handle(req);
  }
  
}
