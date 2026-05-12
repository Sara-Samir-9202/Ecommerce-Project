import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal, WritableSignal } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { WishlistRes } from '../../features/wishlist/models/wishlist-res.interface';
import { wishlistRes } from '../models/wishlistRes.interface';

@Injectable({
  providedIn: 'root'
})
export class WishlistService {


  private readonly httpClient = inject(HttpClient);

  numOfwishlistItems : WritableSignal <number> = signal<number> (0); 

  addProductToWishlist(productId: string ): Observable<wishlistRes> {
    return this.httpClient.post<wishlistRes>(environment.baseURL + 'wishlist', 
      {
        productId: productId
      }
      
    );
  }

  getLoggedUserWishlist(): Observable<WishlistRes> {    
    return this.httpClient.get<WishlistRes>(environment.baseURL + 'wishlist'  )
  }


  removeProductFromWidhlist(productId: string ): Observable<wishlistRes> {
  return this.httpClient.delete<wishlistRes>( environment.baseURL + `wishlist/${productId}` )
}


}
