import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { CardComponent } from "../../../shared/components/card/card.component";
import { ProductService } from '../../../core/services/product.service';
import { Datum, ProductRes } from '../../../core/models/product-res.interface';
import { ActivatedRoute } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { TranslatePipe } from '@ngx-translate/core';

@Component({
  selector: 'app-popular-product',
  imports: [CardComponent, TranslatePipe],
  templateUrl: './popular-product.component.html',
  styleUrl: './popular-product.component.css'
})
export class PopularProductComponent implements OnInit, OnDestroy {

  private readonly productService = inject(ProductService);
  private readonly activatedRoute = inject(ActivatedRoute);

  productList: Datum[] = {} as Datum[];
  productSub$ = new Subject();

  ngOnInit(): void {
    this.getProducts();
  }


  getProducts(): void {
    this.activatedRoute.data.pipe(takeUntil(this.productSub$)).subscribe(response => {
      this.productList = response['product'] ?? []
    })
  }



  ngOnDestroy(): void {
    this.productSub$.next(0);
    this.productSub$.complete();
  }



}
