import { Component, inject, Input } from '@angular/core';
import { CartService } from '../../../core/services/cart.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-btn',
  imports: [],
  templateUrl: './add-btn.component.html',
  styleUrl: './add-btn.component.css'
})
export class AddBtnComponent {



  private readonly cartService = inject(CartService);
  private readonly toastrService = inject(ToastrService);

  @Input({ required: true }) productId !: string | null;




  addProductToCart(): void {
    this.cartService.addProductToCart(this.productId).subscribe({

      next: (res => {
        console.log(res);
        if (res.status === 'success')
          this.toastrService.success(res.message, 'SHIPSHOP', { positionClass: 'toast-top-left' });
        this.cartService.numOfCartItems.set(res.numOfCartItems);

      })
    })
  }

}
