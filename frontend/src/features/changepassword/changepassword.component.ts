import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { InputComponent } from "../../shared/components/input/input.component";
import { AuthService } from '../../core/auth/service/auth.service';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { TranslatePipe } from '@ngx-translate/core';

@Component({
  selector: 'app-changepassword',
  imports: [ReactiveFormsModule, TranslatePipe, InputComponent],
  templateUrl: './changepassword.component.html',
  styleUrl: './changepassword.component.css'
})
export class ChangepasswordComponent implements OnInit, OnDestroy {

  private readonly fb = inject(FormBuilder)
  private readonly authService = inject(AuthService)
  private readonly cookieService = inject(CookieService)
  private readonly router = inject(Router)

  changePasswordForm !: FormGroup;
  changePassSub !: Subscription;

  ngOnInit(): void {
    this.initForm();
  }

  initForm() {
    this.changePasswordForm = this.fb.group({
      currentPassword: [null, [Validators.required, Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$/)]],
      password: [null, [Validators.required, Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$/)]],
      rePassword: [null, [Validators.required,]],
    }, { validators: this.confirmPassword })
  }


  confirmPassword(group: AbstractControl): any {
    if (group.get('password')?.value === group.get('rePassword')?.value) return null
    group.get('rePassword')?.setErrors({ mismatch: true });
    return { mismatch: true };
  }









  updateLoggedUserPassword(): void {
    if (this.changePasswordForm.valid) {

      if (this.changePassSub) this.changePassSub.unsubscribe();
      console.log(this.changePasswordForm);
      this.changePassSub = this.authService.updateLoggedUserPassword(this.changePasswordForm.value).subscribe({
        next: (res => {
          if (res.message === 'success')
            this.cookieService.set('token', res.token);
          this.router.navigate(['/home']);
          console.log(res);

        })
      })
    }
  }

  ngOnDestroy(): void {
    this.changePassSub.unsubscribe();
  }
}
