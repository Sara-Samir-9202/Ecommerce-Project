import { Component, inject, Input, OnInit } from '@angular/core';
import { WishlistService } from '../../core/services/wishlist.service';
import { Datum } from './models/wishlist-res.interface';
import { CardComponent } from "../../shared/components/card/card.component";
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute } from '@angular/router';
import { TranslatePipe } from '@ngx-translate/core';

@Component({
  selector: 'app-wishlist',
  imports: [CardComponent, TranslatePipe],
  templateUrl: './wishlist.component.html',
  styleUrl: './wishlist.component.css'
})
export class WishlistComponent implements OnInit {


  private readonly wishlistService = inject(WishlistService);
  private readonly toastrService = inject(ToastrService);
  private readonly activatedRoute = inject(ActivatedRoute);

  productWishlist: Datum[] = {} as Datum[];

  ngOnInit(): void {



    this.activatedRoute.data.subscribe(data => {
      this.productWishlist = data['wishlist'] ?? []
    })
  }

  getLoggedUserWishlist(): void {
    this.wishlistService.getLoggedUserWishlist().subscribe({
      next: (res => {
        console.log(res);
        if (res.status === 'success') {
          this.productWishlist = res.data;
        }
      })
    })
  }



  removeProductFromWidhlist(productId: string): void {
    this.wishlistService.removeProductFromWidhlist(productId).subscribe({
      next: (res => {
        console.log(res);
        if (res.status === 'success') {
          this.toastrService.success(res.message, 'SHIPSHOP', { positionClass: 'toast-top-left' })
          this.wishlistService.numOfwishlistItems.set(res.data.length);
          this.getLoggedUserWishlist();

        }
      })
    })

  }






}
