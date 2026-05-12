import { HttpClient, httpResource } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Orders } from '../models/orders.interface';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {
  private readonly httpClient = inject(HttpClient); 

  getUserOrders(userId:string |undefined): Observable<Orders[]> {
    return this.httpClient.get<Orders[]>(environment.baseURL + `orders/user/${userId}`); 
}



}
