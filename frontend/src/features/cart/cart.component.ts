import { Component, inject, OnInit } from '@angular/core';
import { CartService } from '../../core/services/cart.service';
import { CartDetails } from '../../core/models/cart-details.interface';
import { CurrencyPipe } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { TranslatePipe } from '@ngx-translate/core';

@Component({
  selector: 'app-cart',
  imports: [CurrencyPipe, TranslatePipe, RouterLink],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css'
})
export class CartComponent implements OnInit {

  private readonly cartService = inject(CartService);
  private readonly activatedRoute = inject(ActivatedRoute);

  cartDetails: CartDetails = {} as CartDetails;
  cartId: string | null = null;

  ngOnInit(): void {

    // this.getLoggedUserCart()
    this.activatedRoute.data.subscribe(data => {
      this.cartDetails = data['cart'] ?? {}
      this.cartId = data['cart']
    })
  }

  getLoggedUserCart(): void {
    this.cartService.getLoggedUserCart().subscribe({
      next: (res => {
        console.log(res);
        if (res.status === 'success')
          this.cartDetails = res.data;
        this.cartId = res.cartId;
      })
    })
  }
  updateCartProductQuantity(productId: string | null, count: number): void {
    this.cartService.updateCartProductQuantity(productId, count).subscribe({
      next: (res => {
        console.log(res);
        if (res.status === 'success')
          this.cartDetails = res.data
      })
    })
  }
  removeSpecificCartItem(productId: string | null): void {
    this.cartService.removeSpecificCartItem(productId).subscribe({
      next: (res => {
        console.log(res);
        if (res.status === 'success')
          this.cartDetails = res.data
        this.cartService.numOfCartItems.set(res.numOfCartItems);
      })
    })

  }

  clearUserCart(): void {
    this.cartService.clearUserCart().subscribe({
      next: (res => {
        console.log(res);
        this.cartDetails = {} as CartDetails;
      })
    })
  }

}
