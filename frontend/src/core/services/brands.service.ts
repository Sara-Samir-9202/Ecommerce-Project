import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { BrandsRes } from '../models/brands-res.interface';

@Injectable({
  providedIn: 'root'
})
export class BrandsService {


  private readonly httpClient = inject(HttpClient); 

  getAllBrands(pageNumber :number = 1): Observable<BrandsRes> {
    return this.httpClient.get<BrandsRes>(environment.baseURL + `brands?page=${pageNumber}`);
  }




  
}
