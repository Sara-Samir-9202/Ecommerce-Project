import { ResolveFn } from '@angular/router';
import { Datum, ProductRes } from '../../core/models/product-res.interface';
import { inject } from '@angular/core';
import { ProductService } from '../../core/services/product.service';
import { map } from 'rxjs';

export const productresolverResolver: ResolveFn<Datum[]> = (route,
  state
) => {
  const productService = inject(ProductService);
  return productService.getAllProducts().pipe( map(   res => res.data    )  )








}
