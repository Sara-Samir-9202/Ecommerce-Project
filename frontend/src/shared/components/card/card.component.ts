import { CurrencyPipe } from '@angular/common';
import { Component, inject, Input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Datum } from '../../../core/models/product-res.interface';
import { WishlistService } from '../../../core/services/wishlist.service';
import { TremPipe } from '../../pipes/trem-pipe';
import { AddBtnComponent } from "../add-btn/add-btn.component";
import { Placeholder } from '../../directives/placeholder';

@Component({
  selector: 'app-card',
  imports: [CurrencyPipe, RouterLink, AddBtnComponent, TremPipe, Placeholder],
  templateUrl: './card.component.html',
  styleUrl: './card.component.css'
})
export class CardComponent {


  private readonly wishlistService = inject(WishlistService);
  private readonly toastrService = inject(ToastrService);

  productWishlist: Datum[] = {} as Datum[];
  @Input() wish: boolean = true;
  @Input({ required: true }) product: Datum = {} as Datum;

  addProductToWishlist(productId: string): void {
    this.wishlistService.addProductToWishlist(productId).subscribe({
      next: (res) => {
        console.log(res);
        if (res.status === 'success') {
          this.toastrService.success(res.message, 'SHIPSHOP ', { positionClass: 'toast-top-left' })
          this.wishlistService.numOfwishlistItems.set(res.data.length);
        }
      }
    })
  }



}
