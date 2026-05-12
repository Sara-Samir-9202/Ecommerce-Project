import { AfterViewInit, Directive, ElementRef, inject, Input, input, OnInit, Renderer2 } from '@angular/core';

@Directive({
  selector: 'img[appPlaceholder]'
})
export class Placeholder  implements OnInit {



  @Input() appPlaceholder !: string;



  private readonly elementRef = inject(ElementRef);
  private readonly renderer = inject(Renderer2); 


  ngOnInit(): void {
  
    this.renderer.setAttribute(this.elementRef.nativeElement, 'src', this.appPlaceholder); 
    
  }





}
