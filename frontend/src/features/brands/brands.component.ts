import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NgxPaginationModule } from 'ngx-pagination';
import { Subject, takeUntil } from 'rxjs';
import { Datum } from '../../core/models/brands-res.interface';
import { BrandsService } from '../../core/services/brands.service';
import { SearchPipe } from '../../shared/pipes/search-pipe';
import { TranslatePipe } from '@ngx-translate/core';

@Component({
  selector: 'app-brands',
  imports: [FormsModule, TranslatePipe, SearchPipe, NgxPaginationModule],
  templateUrl: './brands.component.html',
  styleUrl: './brands.component.css'
})
export class BrandsComponent implements OnInit, OnDestroy {

  private readonly brandsService = inject(BrandsService);
  private readonly activatedRoute = inject(ActivatedRoute);

  brandsList: Datum[] = [];
  searchInput !: string;
  itemsPerPage!: number;
  currentPage!: number;
  totalItems!: number;

  brandSub$ = new Subject();

  ngOnInit(): void {
    this.getBrandPageOne();

  }

  getBrandPageOne() {
    this.activatedRoute.data.pipe(takeUntil(this.brandSub$)).subscribe(data => {
      this.brandsList = data['brand'] ?? []
    })
  }

  getAllBrands(pageNumber: number = 1): void {
    this.brandsService.getAllBrands(pageNumber).subscribe({
      next: (res => {
        console.log(res);
        this.brandsList = res.data;
        this.itemsPerPage = res.metadata.limit;
        this.currentPage = res.metadata.currentPage;
        this.totalItems = res.results;

      })
    })
  }

  ngOnDestroy(): void {
    this.brandSub$.next(0);
    this.brandSub$.complete();
  }




}
