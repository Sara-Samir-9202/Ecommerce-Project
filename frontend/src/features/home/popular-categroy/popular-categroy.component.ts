import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { Datum } from '../../../core/models/categroy-res.interface';
import { TranslatePipe } from '@ngx-translate/core';

@Component({
  selector: 'app-popular-categroy',
  imports: [TranslatePipe],
  templateUrl: './popular-categroy.component.html',
  styleUrl: './popular-categroy.component.css'
})
export class PopularCategroyComponent implements OnInit, OnDestroy {

  private readonly activatedRoute = inject(ActivatedRoute);

  categroiesDataList: Datum[] = {} as Datum[];
  categroySub$ = new Subject();

  ngOnInit(): void {
    this.categroiesDataList = this.activatedRoute.snapshot.data['categroy'] ?? []

  }

  getCategroies(): void {
    this.activatedRoute.data.pipe(takeUntil(this.categroySub$)).subscribe(
      resposne => {
        this.categroiesDataList = resposne['categroy'] ?? []
      }
    )
  }

  ngOnDestroy(): void {
    this.categroySub$.next(0);
    this.categroySub$.complete();
  }





}
