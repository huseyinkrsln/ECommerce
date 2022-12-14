import { Component, OnInit, ViewChild } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, Spinnertype } from 'src/app/base/base.component';
import { Create_Product } from 'src/app/contracts/create_product';
import { HttpClientService } from 'src/app/services/common/http-client.service';
import { ListComponent } from './list/list.component';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss'],
})
export class ProductsComponent extends BaseComponent implements OnInit {
  constructor(
    spinner: NgxSpinnerService,
    private httpclientService: HttpClientService
  ) {
    super(spinner);
  }

  ngOnInit(): void {
    /**********************Bu kısım örnek amaçlı yazılmıştır***************
    Best Practice için Get-te kullanıcak Contract-o-ı oluşturduk ve türünü buraya yazdık Product[] liste old için
     this.httpclientService
       .get<Create_Product[]>({
         controller: 'products',
       })
       .subscribe((data) => console.log(data));

     this.httpclientService.post({
       controller : "products"
     },{
       name:"Kalem",
       stock:100,
       price:15,
     }).subscribe();

       this.httpclientService
         .put(
           {
             controller: 'products',
           },
           {
             id: '09761253-14a7-4875-a890-8dfc8bb67ccc',
             name: 'deneme',
             stock: 32,
             price: 21,
           }
         )
         .subscribe();

       this.httpclientService
         .delete(
           {
             controller: 'products',
           },
           '09761253-14a7-4875-a890-8dfc8bb67ccc'
         )
         .subscribe();

     this.httpclientService
       .get({
         fullEndPoint:"https://jsonplaceholder.typicode.com/posts"
       })
       .subscribe((x) => console.log(x));
     */
  }


  //html de list ve create child oldkları için @ViewChild ile elde edebiliriz
  @ViewChild(ListComponent) listComponents : ListComponent
  createdProduct(createProduct : Create_Product){
    this.listComponents.getProducts();
  }
}
