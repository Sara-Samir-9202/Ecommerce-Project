import { Component, inject, OnInit } from '@angular/core';
import { AuthService } from '../service/auth.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms'
import { InputComponent } from "../../../shared/components/input/input.component";
import { Router, RouterLink } from '@angular/router';
import { Subscription } from 'rxjs';
import { TitleCasePipe } from '@angular/common';
import { CookieService } from 'ngx-cookie-service';
import { TranslatePipe } from '@ngx-translate/core';
@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule, TranslatePipe, InputComponent, TitleCasePipe, RouterLink],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {

  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);
  private readonly cookieService = inject(CookieService);
  private readonly fb = inject(FormBuilder);
  callAPI: boolean = false;
  logInSub: Subscription = new Subscription();
  errorMsg: string = '';


  loginForm !: FormGroup;


  ngOnInit(): void {
    this.initForm();
  }


  initForm(): void {
    this.loginForm = this.fb.group({
      email: [null, [Validators.required, Validators.email]],
      password: [null, [Validators.required, Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$/)]]
    })
  }







  login(): void {
    if (this.loginForm.valid) {
      console.log(this.loginForm);

      if (this.logInSub) {
        this.logInSub.unsubscribe();
      }


      this.callAPI = true;
      this.logInSub = this.authService.login(this.loginForm.value).subscribe({
        next: (res => {
          console.log(res);
          if (res.message === 'success') {

            //1. save token
            this.cookieService.set('token', res.token);
            // navigate to home
            setTimeout(() => {
              this.router.navigate(['/home']);

            }, 1500);
          }







          this.callAPI = false;
        }),
        error: (err => {
          console.log(err);
          this.errorMsg = err.error.message;

          this.callAPI = false;
        })

      })








    } else {
      this.loginForm.markAllAsTouched();
    }

  }










}
