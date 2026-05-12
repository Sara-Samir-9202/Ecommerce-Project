import { TitleCasePipe } from '@angular/common';
import { Component, Input } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
@Component({
  selector: 'app-input',
  imports: [TitleCasePipe, ReactiveFormsModule],
  templateUrl: './input.component.html',
  styleUrl: './input.component.css'
})
export class InputComponent {


  @Input({ required: true }) control: any;

  @Input() type !: string;

  @Input() element: string = 'input'

  @Input() label !: string;

  @Input({ required: true }) id !: string;

  flagPass: boolean = false;
}
