import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Datum } from '../../core/models/categroy-res.interface';
import { Subject, Subscription, takeUntil } from 'rxjs';
import { TranslatePipe } from '@ngx-translate/core';

@Component({
  selector: 'app-categroies',
  imports: [TranslatePipe],
  templateUrl: './categroies.component.html',
  styleUrl: './categroies.component.css'
})
export class CategroiesComponent implements OnInit, OnDestroy {


  private readonly activatedRoute = inject(ActivatedRoute);
  categroiesList: Datum[] = [];
  categroySub$ = new Subject();



  ngOnInit(): void {
    this.getAllCategroies();
  }

  getAllCategroies(): void {
    this.activatedRoute.data.pipe(takeUntil(this.categroySub$)).subscribe(data => {
      this.categroiesList = data['categroy'] ?? []
    })
  }

  ngOnDestroy(): void {
    this.categroySub$.next(0);
  }




}
