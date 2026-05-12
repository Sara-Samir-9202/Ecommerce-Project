import { TitleCasePipe } from '@angular/common';
import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { Subscription } from 'rxjs';
import { InputComponent } from "../../../shared/components/input/input.component";
import { AuthService } from '../service/auth.service';
import { TranslatePipe } from '@ngx-translate/core';


@Component({
  selector: 'app-signup',
  imports: [ReactiveFormsModule, TranslatePipe, InputComponent, TitleCasePipe, RouterLink],
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.css'
})
export class SignupComponent implements OnInit, OnDestroy {

  private readonly authService = inject(AuthService);
  private readonly fb = inject(FormBuilder);
  private readonly cookieService = inject(CookieService);
  private readonly router = inject(Router);
  signupForm !: FormGroup;
  private signupSub: Subscription = new Subscription();
  errorMsg: string = '';
  callAPI: boolean = false;


  ngOnInit(): void {
    this.initForm();
  }


  initForm() {
    this.signupForm = this.fb.group({
      name: [null, [Validators.required, Validators.minLength(3), Validators.maxLength(20)]],
      email: [null, [Validators.required, Validators.email]],
      password: [null, [Validators.required, Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$/)]],
      rePassword: [null, Validators.required],
      phone: [null, [Validators.required, Validators.pattern(/^01[0125][0-9]{8}$/)]]
    }, { validators: this.verifyPassword })
  }



  verifyPassword(group: AbstractControl): any {
    if (group.get('password')?.value === group.get('rePassword')?.value) return null;
    else {
      group.get('rePassword')?.setErrors({ mismatch: true });
      return { mismatch: true };
    }
  }



  signup() {
    if (this.signupForm.valid) {
      console.log(this.signupForm);

      this.callAPI = true;


      if (this.signupSub) {
        this.signupSub.unsubscribe();
      }


      this.signupSub = this.authService.signup(this.signupForm.value).subscribe({
        next: (res => {
          console.log(res);
          if (res.message === 'success') {
            //1. save token
            this.cookieService.set('token', res.token);

            //2.navigate to path login
            setTimeout(() => {

              this.router.navigate(['/login']);
            }, 1500);

          }
          this.callAPI = false;

        }),

        error: (err => {
          console.log(err);
          this.errorMsg = err.error.message;
          this.callAPI = false
        })

      })
    }
    else {

      this.signupForm?.get('rePassword')?.patchValue('');
      this.signupForm.markAllAsTouched();
    }
  }



  ngOnDestroy(): void {
    this.signupSub.unsubscribe();
  }

}
