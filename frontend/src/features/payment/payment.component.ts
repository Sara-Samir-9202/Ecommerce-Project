import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { InputComponent } from "../../shared/components/input/input.component";
import { CartService } from '../../core/services/cart.service';
import { CartDetails } from '../../core/models/cart-details.interface';
import { CurrencyPipe } from '@angular/common';
import { CheckoutService } from './service/checkout.service';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslatePipe } from '@ngx-translate/core';


@Component({
  selector: 'app-payment',
  imports: [InputComponent, TranslatePipe, CurrencyPipe, ReactiveFormsModule],
  templateUrl: './payment.component.html',
  styleUrl: './payment.component.css'
})
export class PaymentComponent implements OnInit {


  private readonly fb = inject(FormBuilder);
  private readonly cartService = inject(CartService);
  private readonly checkoutService = inject(CheckoutService);
  private readonly activatedRoute = inject(ActivatedRoute);
  private readonly router = inject(Router);

  cartDetails: CartDetails = {} as CartDetails;
  checkoutForm!: FormGroup;
  cartId: string | null = null;

  ngOnInit(): void {
    this.getCartIDd();
    this.initForm();
    this.getLoggedUserCart();
  }


  initForm(): void {
    this.checkoutForm = this.fb.group({
      shippingAddress: this.fb.group({
        details: [null, [Validators.required]],
        phone: [null, [Validators.required, Validators.pattern(/^01[0125][0-9]{8}$/)]],
        city: [null, [Validators.required]]
      })
    })
  }



  getLoggedUserCart(): void {
    this.cartService.getLoggedUserCart().subscribe({
      next: (res => {
        console.log(res);
        this.cartDetails = res.data
      }),
      error: (err => {
        console.log(err);
      })
    })
  }

  getCartIDd(): void {

    this.activatedRoute.paramMap.subscribe({
      next: (urlParams => {
        console.log(urlParams);
        this.cartId = urlParams.get('cartId');
      })
    })
  }






  checkoutUp(event: SubmitEvent): void {

    if (this.checkoutForm.valid) {
      const btn = (event.submitter as HTMLButtonElement)?.value;


      if (btn === 'online') {
        this.checkoutService.checkoutSession(this.cartId, this.checkoutForm.value).subscribe({
          next: (res => {
            console.log(res);
            if (res.status === 'success') {
              window.open(res.session.url, '_self');
            }
          }),

        })

      } else {
        this.checkoutService.createCacheOrder(this.cartId, this.checkoutForm.value).subscribe({
          next: (res => {
            console.log(res);
            if (res.status === 'success') {
              this.router.navigate(['/allorders'])
            }

          }),

        })


      }

    } else {
      this.checkoutForm.markAllAsTouched();
    }
  }



}
