import { Component, ElementRef, inject, OnDestroy, OnInit, Renderer2, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { InputComponent } from "../../../shared/components/input/input.component";
import { CartService } from '../../services/cart.service';
import { AuthService } from '../service/auth.service';
import { error } from 'console';
import { ToastrService } from 'ngx-toastr';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { subscribe } from 'diagnostics_channel';
import { TremPipe } from '../../../shared/pipes/trem-pipe';
import { TranslatePipe } from '@ngx-translate/core';

@Component({
  selector: 'app-forgotpassword',
  imports: [InputComponent, TranslatePipe, ReactiveFormsModule],
  templateUrl: './forgotpassword.component.html',
  styleUrl: './forgotpassword.component.css'
})
export class ForgotpasswordComponent implements OnInit, OnDestroy {

  @ViewChild('email') emailEle !: ElementRef;
  private readonly authService = inject(AuthService);
  private readonly fb = inject(FormBuilder);
  private readonly toastrService = inject(ToastrService);
  private readonly cookieService = inject(CookieService);
  private readonly router = inject(Router);


  forgotPasswordForm !: FormGroup;
  verifyResetCodeForm !: FormGroup;
  resetPasswordForm !: FormGroup;
  stepper: number = 1;
  email !: string;

  forgetPassSub: Subscription = new Subscription();
  verifyResetCodeSub: Subscription = new Subscription;
  resetPassSub: Subscription = new Subscription;


  ngOnInit() {
    this.initForm();
  }


  initForm() {
    this.forgotPasswordForm = this.fb.group({
      email: [null, [Validators.required, Validators.email]]
    })


    this.verifyResetCodeForm = this.fb.group({
      resetCode: [null, [Validators.required, Validators.pattern(/^\w{4,10}$/)]]
    })


    this.resetPasswordForm = this.fb.group({
      email: [null, [Validators.required, Validators.email]],
      newPassword: [null, [Validators.required, Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$/)]]
    })
  }




  forgetPassword(): void {
    if (this.forgotPasswordForm.valid) {

      if (this.forgetPassSub) this.forgetPassSub.unsubscribe();

      this.email = this.forgotPasswordForm.get('email')?.value;
      console.log(this.forgotPasswordForm);
      this.forgetPassSub = this.authService.forgotPassword(this.forgotPasswordForm.value).subscribe({
        next: (res => {
          if (res.statusMsg === 'success') {
            this.toastrService.success(res.message, 'SHIPSHOP')
            console.log(res);
            this.stepper = 2
          }
        })
      })
    }
  }


  verifyResetCode(): void {
    if (this.verifyResetCodeForm.valid) {

      if (this.verifyResetCodeSub) this.verifyResetCodeSub.unsubscribe();

      this.verifyResetCodeSub = this.authService.verifyResetCode(this.verifyResetCodeForm.value).subscribe({
        next: (res => {
          if (res.status === 'Success') {

            console.log(res);
            this.stepper = 3;
            this.resetPasswordForm.get('email')?.patchValue(this.email);
            // this.renderer.setAttribute(this.emailEle.nativeElement, 'disabled', 'true');           
          }
        }),
      })
    }
  }


  resetPassword(): void {
    if (this.resetPasswordForm.valid) {
      if (this.resetPassSub) this.resetPassSub.unsubscribe();

      this.resetPassSub = this.authService.resetPassword(this.resetPasswordForm.value).subscribe({
        next: (res => {
          this.cookieService.set('token', res.token)
          this.router.navigate(['/home']);
          console.log(res);
        })
      })
    }
  }





  ngOnDestroy(): void {
    this.forgetPassSub.unsubscribe();
    this.verifyResetCodeSub.unsubscribe()
    this.resetPassSub.unsubscribe();

  }














}
