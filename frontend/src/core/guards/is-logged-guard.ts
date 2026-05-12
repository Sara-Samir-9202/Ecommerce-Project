import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { AuthService } from '../auth/service/auth.service';
import { map } from 'rxjs';

export const isLoggedGuard: CanActivateFn = (route, state) => {


  const cookieService = inject(CookieService);
  const router = inject(Router);
  const authService = inject(AuthService);

 
  if (cookieService.get('token')) {
    return authService.verifyToken().pipe(
      map(res => {
        if (res.message === 'verified') {
          return true;
        }
        return router.parseUrl('/login');
      })
    )
  }
  return router.parseUrl('/login');
}
  
