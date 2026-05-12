import { inject } from '@angular/core';
import { ResolveFn } from '@angular/router';
import { CategroyService } from '../../core/services/categroy.service';
import { map } from 'rxjs';
import { Datum } from '../../core/models/categroy-res.interface';

export const categroyResolver: ResolveFn<Datum [] > = (route, state) => {



  const categroyService = inject(CategroyService); 




  return categroyService.getAllCategroies().pipe(  
    map ( res => res.data )
  );

};
