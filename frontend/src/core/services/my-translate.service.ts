import { isPlatformBrowser } from '@angular/common';
import { Inject, Injectable, PLATFORM_ID, Renderer2, RendererFactory2 } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { TranslateService } from '@ngx-translate/core'
@Injectable({
  providedIn: 'root'
})
export class MyTranslateService {


  private renderer2: Renderer2;

  //  علشان اول ما افتح الابليكيشن تظهر اللغه اللي انا مختاراها 
  constructor(private translateService: TranslateService, private rendererFactory2: RendererFactory2,
    @Inject(PLATFORM_ID) private ID: object
  ) {
    this.renderer2 = rendererFactory2.createRenderer(null, null);

    if (isPlatformBrowser(this.ID)) {


      //#region  translate Words 
      // 1. set default lang
      this.translateService.setFallbackLang('en');

      //2. take lang from  localstorage
      const savedLang = localStorage.getItem('lang');

      //3.use lang from localstorage 
      if (savedLang)
        translateService.use(savedLang);

      //#endregion


      //#region change direction 
      this.changeDirection();


      //#endregion

    }
  }


  changeDirection() {

    if (localStorage.getItem('lang') === 'en') {
      //  change dir and lang attr on html tag

      this.renderer2.setAttribute(document.documentElement, 'dir', 'ltr')
      this.renderer2.setAttribute(document.documentElement, 'lang', 'en')
      localStorage.setItem('dir', 'ltr');

    }
    else if (localStorage.getItem('lang') === 'ar') {
      //  change dir and lang attr on html tag

      this.renderer2.setAttribute(document.documentElement, 'dir', 'rtl')
      this.renderer2.setAttribute(document.documentElement, 'lang', 'ar')

      localStorage.setItem('dir', 'rtl');

    }


  }



  changeLang(lang: string): void {
    // 1.save lang in localStorage 
    localStorage.setItem('lang', lang);

    // 2.use lang from localStorage 
    const savedLang = localStorage.getItem('lang');
    if (savedLang)
      this.translateService.use(savedLang);

    // 3.change direction
    this.changeDirection();

  }



}
