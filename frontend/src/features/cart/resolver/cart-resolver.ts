import { inject } from '@angular/core';
import { Data, ResolveFn } from '@angular/router';
import { CartService } from '../../../core/services/cart.service';
import { map } from 'rxjs';

export const cartResolver: ResolveFn<Data> = (route, state) => {


  const cartService = inject(CartService); 
  return cartService.getLoggedUserCart().pipe(  map( res => res.data)  ); 




};
