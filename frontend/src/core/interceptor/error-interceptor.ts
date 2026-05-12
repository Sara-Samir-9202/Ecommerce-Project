import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { catchError, throwError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {

  const toastrService = inject(ToastrService);


  return next(req).pipe(catchError
    (error => {
      console.log(error);
      toastrService.error(error.error.message, 'SHIPSHOP');
      return throwError(() => error)
    }));
};
