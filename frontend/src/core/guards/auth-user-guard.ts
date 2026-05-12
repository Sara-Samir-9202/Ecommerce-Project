import { inject } from '@angular/core';
import { CanDeactivateFn, Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { AuthService } from '../auth/service/auth.service';
import { map } from 'rxjs';

export const authUserGuard: CanDeactivateFn<unknown> = (component, currentRoute, currentState, nextState) => {



  const cookieService = inject(CookieService);
  const authService = inject(AuthService);



  if (cookieService.get('token')) {
    return authService.verifyToken().pipe(
      map(res => {
        if (res.message === 'verified') {
          return false;
        }
        return true;
      })
    )
  }
  return true;
};
