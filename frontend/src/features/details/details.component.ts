import { Component, inject, OnInit   } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CurrencyPipe } from '@angular/common';
import { initFlowbite } from 'flowbite';
import { FlowbiteService } from '../../core/services/flowbite.service';
import { AddBtnComponent } from "../../shared/components/add-btn/add-btn.component";
import { Datum } from '../../core/models/product-res.interface';
import { CartService } from '../../core/services/cart.service';
import { Placeholder } from '../../shared/directives/placeholder';


@Component({
  selector: 'app-details',
  imports: [CurrencyPipe, AddBtnComponent ],
  templateUrl: './details.component.html',
  styleUrl: './details.component.css',

})
export class DetailsComponent implements OnInit  {
  constructor(private flowbiteService: FlowbiteService) {}


  private readonly activatedRoute = inject(ActivatedRoute); 
  private readonly cartService = inject(CartService); 
  productId: string | null = null;
  productData: Datum = {} as Datum; 



  ngOnInit(): void {
     this.flowbiteService.loadFlowbite((flowbite) => {
      initFlowbite();
    });
    
    this.getProductId();
    
    
    this.activatedRoute.data.subscribe(data => {
      this.productData = data['details' ] ?? {}
    })
    
  }

  getProductId(): void {
    this.activatedRoute.paramMap.subscribe({
      next: (urlParams => {
        console.log(urlParams);
        this.productId = urlParams.get('id'); 
      })
    })
  }






  


  }







