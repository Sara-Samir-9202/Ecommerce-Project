import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'trem'
})
export class TremPipe implements PipeTransform {

  transform(  name  : string , numOfWords : number    ): string  {
    return     name.split(' ' , numOfWords).join(' ')  ;
  }

}
