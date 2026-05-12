import { Component, inject, OnInit } from '@angular/core';
import { AuthService } from '../../core/auth/service/auth.service';
import { OrdersService } from './services/orders.service';
import { Orders, User } from './models/orders.interface';
import { CurrencyPipe, DatePipe } from '@angular/common';
import { TranslatePipe } from '@ngx-translate/core';

@Component({
  selector: 'app-allorders',
  imports: [DatePipe, TranslatePipe, CurrencyPipe],
  templateUrl: './allorders.component.html',
  styleUrl: './allorders.component.css'
})
export class AllordersComponent implements OnInit {


  private readonly authService = inject(AuthService);

  private readonly ordersService = inject(OrdersService);


  userId: string | undefined = this.authService.decode()?.id
  orders: Orders[] = [];
  userOrder: User = {} as User;

  ngOnInit(): void {

    this.getUserOrders();
  }

  getUserOrders(): void {
    this.ordersService.getUserOrders(this.userId).subscribe({
      next: (res => {
        console.log(res);
        this.orders = res;
        this.userOrder.name = res[0].user.name
      }),
      error: (err => {
        console.log(err);

      })
    })
  }



}
