import { Component, computed, ElementRef, HostListener, inject, OnInit, Signal, ViewChild } from '@angular/core';
import { FlowbiteService } from '../../../core/services/flowbite.service';
import { initFlowbite } from 'flowbite';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { AuthService } from '../../../core/auth/service/auth.service';
import { CartService } from '../../../core/services/cart.service';
import { WishlistService } from '../../../core/services/wishlist.service';
import { TranslatePipe, TranslateService } from '@ngx-translate/core';
import { MyTranslateService } from '../../../core/services/my-translate.service';

@Component({
  selector: 'app-navbar',
  imports: [RouterLink, RouterLinkActive, TranslatePipe],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit {
  @ViewChild('nav') navElement !: ElementRef;
  private readonly cartService = inject(CartService);
  private readonly wishlistService = inject(WishlistService);

  numOfCartItems: Signal<number> = computed<number>(() => this.cartService.numOfCartItems())

  numOfWishlistItems: Signal<number> = computed<number>(() => this.wishlistService.numOfwishlistItems())

  @HostListener('window:scroll')
  scroll(): void {
    if (scrollY > 0) {
      this.navElement.nativeElement.style.position = 'fixed';
      this.navElement.nativeElement.style.right = '0';
      this.navElement.nativeElement.style.top = '0';
      this.navElement.nativeElement.style.left = '0';
      this.navElement.nativeElement.style.zIndex = '999';
      this.navElement.nativeElement.style.boxShadow = '0 4px 6px rgba(0,0,0,0.2)';
    } else {
      this.navElement.nativeElement.style.position = 'relative';
      this.navElement.nativeElement.style.boxShadow = 'none';
    }
  }

  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);
  private readonly myTranslateService = inject(MyTranslateService);
  private readonly translateService = inject(TranslateService);

  currentLang: string;



  constructor(private flowbiteService: FlowbiteService) {
    this.currentLang = this.translateService.getCurrentLang();
    console.log(this.currentLang);

  }

  ngOnInit(): void {
    this.flowbiteService.loadFlowbite((flowbite) => {
      initFlowbite();
    });
    this.getLoggedUserCart();
    this.getLoggedUserWishList();
  }


  signOut(): void {
    this.authService.signOut();
  }

  showSearchInput(): boolean {
    const url = this.router.url;
    return url.includes('products') || url.includes('brands') || url.includes('categroy');
  }


  getLoggedUserCart(): void {
    this.cartService.getLoggedUserCart().subscribe({
      next: (res => {
        this.cartService.numOfCartItems.set(res.numOfCartItems);
      })
    })
  }



  getLoggedUserWishList(): void {
    this.wishlistService.getLoggedUserWishlist().subscribe({
      next: (res => {
        this.wishlistService.numOfwishlistItems.set(res.data.length);
      })
    })
  }



  changeLang(lang: string) {
    this.myTranslateService.changeLang(lang);
  }

}
