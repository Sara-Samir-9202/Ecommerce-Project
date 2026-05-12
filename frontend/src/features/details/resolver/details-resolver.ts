import { inject } from '@angular/core';
import { ActivatedRoute, ResolveFn } from '@angular/router';
import { ProductService } from '../../../core/services/product.service';
import { Datum, ProductRes } from '../../../core/models/product-res.interface';
import { map } from 'rxjs';

export const detailsResolver: ResolveFn<Datum[]> = (route, state) => {


  const productService = inject(ProductService); 

    let productId =  route.paramMap.get('id')!; 

console.log(  state.url );
  return productService.getSpecificProduct(productId).pipe( 
    map(res => res.data  ) 
  ); 



};
