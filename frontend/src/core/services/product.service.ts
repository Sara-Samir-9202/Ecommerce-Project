import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Datum, ProductRes } from '../models/product-res.interface';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  
  private readonly httpClient = inject(HttpClient);


  getAllProducts(pageNumber:number = 1): Observable<ProductRes> {
    return this.httpClient.get<ProductRes>(environment.baseURL + `products?page=${pageNumber}`);
}
  
  
  getSpecificProduct(productId : string |null  ): Observable<ProductRes> {
    return this.httpClient.get<ProductRes>(environment.baseURL + `products/${productId}`); 
  }
  










}
