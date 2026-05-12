import { Component } from '@angular/core';
import { TranslatePipe } from '@ngx-translate/core';

@Component({
  selector: 'app-static-products',
  imports: [TranslatePipe],
  templateUrl: './static-products.component.html',
  styleUrl: './static-products.component.css'
})
export class StaticProductsComponent {

}
