import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal, WritableSignal } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { CookieService } from 'ngx-cookie-service';
import { AddProdRes } from '../models/add-prod-res.interface';
import { UsrtCartRes } from '../models/usrt-cart-res.interface';

@Injectable({
  providedIn: 'root'
})
export class CartService {



  private readonly httpClient = inject(HttpClient);
  numOfCartItems : WritableSignal<number> = signal<number>(0); 



  addProductToCart(productId: string | null): Observable<AddProdRes> {
    return this.httpClient.post<AddProdRes>(environment.baseURL + 'cart',
      {
        productId: productId
      }
    )
  }





  getLoggedUserCart(): Observable<UsrtCartRes> {
    return this.httpClient.get<UsrtCartRes>(environment.baseURL + 'cart');
  }




  updateCartProductQuantity(productId:string | null ,count:number ): Observable<UsrtCartRes>{
    return this.httpClient.put<UsrtCartRes>(environment.baseURL + `cart/${productId}`, {
      count: count
    } )
  }


  removeSpecificCartItem(productId:string |null ): Observable<UsrtCartRes> {
    return this.httpClient.delete<UsrtCartRes>(environment.baseURL + `cart/${productId}` )
  }


  clearUserCart(): Observable<any> {
    return this.httpClient.delete(environment.baseURL + 'cart');
  }

}
