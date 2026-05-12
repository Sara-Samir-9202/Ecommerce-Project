import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { CategroyRes } from '../models/categroy-res.interface';

@Injectable({
  providedIn: 'root'
})
export class CategroyService {

  private readonly httpClient = inject(HttpClient);



  getAllCategroies() : Observable<CategroyRes> {
    return this.httpClient.get<CategroyRes>(environment.baseURL + 'categories');
  }


}
