import { inject } from '@angular/core';
import { ResolveFn } from '@angular/router';
import { BrandsService } from '../../core/services/brands.service';
import { map, takeUntil } from 'rxjs';
import { Datum } from '../../core/models/brands-res.interface';

export const brandsResolver: ResolveFn<Datum[]> = (route, state) => {



  const brandsService = inject(BrandsService);

  return brandsService.getAllBrands().pipe(
    map(res => res.data)
  );





};
