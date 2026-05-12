import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable, retry } from 'rxjs';
import { environment } from '../../../environments/environment';
import { authRes } from '../model/auth-res.interface';
import { CookieService } from 'ngx-cookie-service';
import { TokenRes } from '../model/token-res.interface';
import { Router } from '@angular/router';
import { jwtDecode } from "jwt-decode";
import { Decodetoken } from '../model/decodetoken.interface';
import { Forgotpassword } from '../model/forgotpassword.interface';
import { Changepass } from '../../../features/changepassword/model/changepass.interface';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private readonly httpClient = inject(HttpClient);
  private readonly cookieService = inject(CookieService);
  private readonly router = inject(Router);



  signup(body: object): Observable<authRes> {
    return this.httpClient.post<authRes>(environment.baseURL + 'auth/signup', body);
  }

  login(body: object): Observable<authRes> {
    return this.httpClient.post<authRes>(environment.baseURL + 'auth/signin', body);
  }


  signOut(): void {
    this.cookieService.delete('token');
    this.router.navigate(['/login']);

  }


  verifyToken(): Observable<TokenRes> {
    return this.httpClient.get<TokenRes>(environment.baseURL + 'auth/verifyToken', {
      headers: {
        token: this.cookieService.get('token')
      }
    });
  }



  decode(): Decodetoken |null {
    try {
      return jwtDecode<Decodetoken>(this.cookieService.get('token'))
    }
    catch (error) {
      console.log(error);
      return null ;
    }
  }


  forgotPassword( body : object ): Observable<Forgotpassword> {
   return  this.httpClient.post<Forgotpassword>( environment.baseURL + 'auth/forgotPasswords' , body )
  }




  verifyResetCode(body: object): Observable<any> {
    return this.httpClient.post(environment.baseURL + 'auth/verifyResetCode', body); 
  }


  resetPassword(body: object): Observable<any> {
    return this.httpClient.put(environment.baseURL + 'auth/resetPassword', body); 
   }

  
  updateLoggedUserPassword(body: object) :Observable<Changepass> {
    return this.httpClient.put<Changepass>(environment.baseURL + 'users/changeMyPassword', body); 
  }

}
