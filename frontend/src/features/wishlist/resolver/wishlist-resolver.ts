import { inject } from '@angular/core';
import { ResolveFn } from '@angular/router';
import { WishlistService } from '../../../core/services/wishlist.service';
import { map } from 'rxjs';
import { Datum } from '../models/wishlist-res.interface';

export const wishlistResolver: ResolveFn< Datum[]  > = (route, state) => {


  const wishlistService = inject(WishlistService); 

  return wishlistService.getLoggedUserWishlist().pipe( 
  
    map( res => res.data )
 )  ; 


};
