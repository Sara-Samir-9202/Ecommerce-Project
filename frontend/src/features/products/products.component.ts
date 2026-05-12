import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NgxPaginationModule } from 'ngx-pagination';
import { Subject, takeUntil } from 'rxjs';
import { Datum } from '../../core/models/product-res.interface';
import { ProductService } from '../../core/services/product.service';
import { CardComponent } from "../../shared/components/card/card.component";
import { SearchPipe } from '../../shared/pipes/search-pipe';
import { TranslatePipe } from '@ngx-translate/core';

@Component({
  selector: 'app-products',
  imports: [CardComponent, TranslatePipe, SearchPipe, FormsModule, NgxPaginationModule],
  templateUrl: './products.component.html',
  styleUrl: './products.component.css',

})
export class ProductsComponent implements OnInit, OnDestroy {


  private readonly productService = inject(ProductService);
  private readonly activatedRoute = inject(ActivatedRoute);


  productList: Datum[] = [];
  searchInput !: string;
  itemsPerPage !: number;
  currentPage !: number;
  totalItems !: number;
  productSub$ = new Subject();

  ngOnInit(): void {
    this.getProductPageOne();
  }


  getProductPageOne() {
    this.activatedRoute.data.pipe(takeUntil(this.productSub$)).subscribe(data => {
      this.productList = data['product'] ?? []
    })
  }

  getAllProducts(pageNumber: number = 1): void {
    this.productService.getAllProducts(pageNumber).subscribe({
      next: (res => {
        console.log(res);
        this.productList = res.data;
        this.itemsPerPage = res.metadata.limit;
        this.currentPage = res.metadata.currentPage;
        this.totalItems = res.results;
      })
    })

  }


  ngOnDestroy(): void {
    this.productSub$.next(0);
    this.productSub$.complete();
  }
}
