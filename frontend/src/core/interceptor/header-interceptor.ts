import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';

export const headerInterceptor: HttpInterceptorFn = (req, next) => {

  const cookieService = inject(CookieService);




  // if (req.url.includes('cart') || req.url.includes('wishlist')  ||  req.url.includes('changepassword') ) {
  if (cookieService.get('token')) {
    req = req.clone({
      setHeaders: {
        token: cookieService.get('token'),
        // lang:  cookieService.get('lang')
      }
    })
  }
  // }




  return next(req);
};
