import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'search'
})
export class SearchPipe implements PipeTransform {


  transform(arry: any[], searchValue: string): any[] {
    if (!arry) return [];
    if (!searchValue) return arry



    return arry.filter(
      item => 
     (item.title && item.title.toLowerCase().includes(searchValue.toLowerCase()) ) || 
     (item.name  && item.name.toLowerCase().includes(searchValue.toLowerCase()) )    
      );
      
  

  
}
}