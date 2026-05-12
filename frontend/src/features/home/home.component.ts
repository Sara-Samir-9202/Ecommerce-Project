import { Component, inject } from '@angular/core';
import { MainSliderComponent } from "./main-slider/main-slider.component";
import { PopularProductComponent } from "./popular-product/popular-product.component";
import { PopularBrandsComponent } from "./popular-brands/popular-brands.component";
import { PopularCategroyComponent } from "./popular-categroy/popular-categroy.component";
import { StaticProductsComponent } from "./static-products/static-products.component";
import { AuthService } from '../../core/auth/service/auth.service';

@Component({
  selector: 'app-home',
  imports: [MainSliderComponent, PopularProductComponent, PopularBrandsComponent, PopularCategroyComponent, StaticProductsComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {


  




}
