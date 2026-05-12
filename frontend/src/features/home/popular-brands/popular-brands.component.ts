import { Component, CUSTOM_ELEMENTS_SCHEMA, inject, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { Datum } from '../../../core/models/brands-res.interface';
import { TranslatePipe } from '@ngx-translate/core';

@Component({
  selector: 'app-popular-brands',
  imports: [TranslatePipe],
  templateUrl: './popular-brands.component.html',
  styleUrl: './popular-brands.component.css',
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class PopularBrandsComponent implements OnInit, OnDestroy {

  private readonly activatedRoute = inject(ActivatedRoute);

  brandsDataList: Datum[] = {} as Datum[];
  brandSub$ = new Subject();

  ngOnInit(): void {
    this.getBrands();
  }




  getBrands(): void {
    this.activatedRoute.data.pipe(takeUntil(this.brandSub$)).subscribe(
      response => {
        this.brandsDataList = response['brand'] ?? [];
      }
    )
  }

  ngOnDestroy(): void {
    this.brandSub$.next(0);
    this.brandSub$.complete();
  }


}





