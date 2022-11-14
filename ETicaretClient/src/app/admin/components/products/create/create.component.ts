import { outputAst } from '@angular/compiler';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, Spinnertype } from 'src/app/base/base.component';
import { Create_Product } from 'src/app/contracts/create_product';
import {
  AlertifyOptions,
  AlertifyService,
  MessageType,
  Position,
} from 'src/app/services/admin/alertify.service';
import { ProductService } from 'src/app/services/common/models/product.service';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss'],
})
export class CreateComponent extends BaseComponent implements OnInit {
  constructor(
    spinner: NgxSpinnerService,
    private productService: ProductService,
    private alertify: AlertifyService
  ) {
    super(spinner);
  }

  ngOnInit(): void {}

  //Ürün oluştuğunda sayfa yenilenmeden tablo güncellenmesi için yani Createden -> Product'a buradan list'e
  @Output() createdProduct : EventEmitter<Create_Product> = new EventEmitter();

  create(
    name: HTMLInputElement,
    stock: HTMLInputElement,
    price: HTMLInputElement
  ) {
    const create_product: Create_Product = new Create_Product();
    this.showSpinner(Spinnertype.ballElasticDots);
    create_product.name = name.value;
    create_product.price = parseFloat(price.value);
    create_product.stock = parseInt(stock.value);

    this.productService.create(
      create_product,
      () => {
        this.hideSpinner(Spinnertype.ballElasticDots);
        this.alertify.message('ürün başarıyla eklenmiştir', {
          dismissOthers: true,
          messageType: MessageType.Success,
          position: Position.TopCenter,
        });
        //başarılı olduğunda createproduct türünde nesneyi fırlatacak ve bunu bir üstündeki compoonente fırlatacak (product component(html dosyasına bak))
        this.createdProduct.emit(create_product); // parametreyei $event ile yakalıyoruz
      },
      errorMessage => {
        this.alertify.message(errorMessage, {
          dismissOthers: true,
          messageType: MessageType.Error,
          position: Position.TopRight,
        });
      }
    );
  }
}
