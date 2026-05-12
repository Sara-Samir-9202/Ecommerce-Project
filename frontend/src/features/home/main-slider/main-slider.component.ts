import { CurrencyPipe } from '@angular/common';
import { Component, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { TranslatePipe } from '@ngx-translate/core';
import { MyTranslateService } from '../../../core/services/my-translate.service';



@Component({
  selector: 'app-main-slider',
  imports: [CurrencyPipe, TranslatePipe],
  templateUrl: './main-slider.component.html',
  styleUrl: './main-slider.component.css',
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class MainSliderComponent {




  dir: string | null;

  constructor() {
    this.dir = localStorage.getItem('dir') ?? '';
    console.log(this.dir);


  }







}
