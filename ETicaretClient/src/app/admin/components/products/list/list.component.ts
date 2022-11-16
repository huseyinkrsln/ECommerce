import { Component, OnInit } from '@angular/core';
import { AfterViewInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, Spinnertype } from 'src/app/base/base.component';
import { List_Product } from 'src/app/contracts/list_product';
import {
  AlertifyService,
  MessageType,
  Position,
} from 'src/app/services/admin/alertify.service';
import { ProductService } from 'src/app/services/common/models/product.service';

//delete işlemi için Jquery talep ettik
declare var $: any;

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss'],
})
export class ListComponent extends BaseComponent implements OnInit {
  constructor(
    spinner: NgxSpinnerService,
    private productsService: ProductService,
    private alertfiyService: AlertifyService
  ) {
    super(spinner);
  }

  displayedColumns: string[] = [
    'name',
    'stock',
    'price',
    'createdDate',
    'updatedDate',
    'edit',
    'delete',
  ];
  // null çünkü api den veriler henüz gelmedi
  dataSource: MatTableDataSource<List_Product> = null;
  @ViewChild(MatPaginator) paginator: MatPaginator;

  async getProducts() {
    this.showSpinner(Spinnertype.ballScaleMultiple);
    const allProducts: { totalCount: number; products: List_Product[] } =
      await this.productsService.read(
        this.paginator ? this.paginator.pageIndex : 0,
        this.paginator ? this.paginator.pageSize : 5,
        () => this.hideSpinner(Spinnertype.ballScaleMultiple),
        (errorMessage) =>
          this.alertfiyService.message(errorMessage, {
            dismissOthers: true,
            messageType: MessageType.Error,
            position: Position.TopCenter,
          })
      );
    this.dataSource = new MatTableDataSource<List_Product>(
      allProducts.products
    );
    this.paginator.length = allProducts.totalCount;
    // this.dataSource.paginator = this.paginator;
  }

  //directive kullanmakdan delete
  //delete de amaç tıkladığımız satırın parentına ulaşmak yani tr'ye
  //   delete(id,event ){
  // const img : HTMLImageElement = event.srcElement;
  // // tr yi 2 sn de kaldırcak
  //   }
  // $(img.parentElement.parentElement).fadeOut(2000);

  async pageChanged() {
    await this.getProducts();
  }
  async ngOnInit() {
    await this.getProducts();
  }
}
