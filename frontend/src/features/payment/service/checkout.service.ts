import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment.development';
import { CookieService } from 'ngx-cookie-service';
import { PaymentRes } from '../models/payment-res.interface';
import { ChechOutRes } from '../models/chech-out-res.interface';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {

  private readonly httpClient = inject(HttpClient);
  private readonly cookieService = inject(CookieService);



  header: object = {
    headers: {
      token: this.cookieService.get('token')
    }
  }




  checkoutSession(cartId:string | null , body:object ): Observable<PaymentRes> {
    return this.httpClient.post<PaymentRes>(environment.baseURL + `orders/checkout-session/${cartId}?url=http://localhost:4200`, 
     body , this.header
   )
  }

  createCacheOrder(cartId:string | null , body:object ): Observable<ChechOutRes> {
    return this.httpClient.post<ChechOutRes>(environment.baseURL + `orders/${cartId}`, body, this.header);
}






}
