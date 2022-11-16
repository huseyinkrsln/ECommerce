import { HttpErrorResponse } from '@angular/common/http';
import {
  Directive,
  ElementRef,
  EventEmitter,
  HostListener,
  Input,
  Output,
  Renderer2,
} from '@angular/core';
import {
  MatDialog,
} from '@angular/material/dialog';
import { NgxSpinnerService } from 'ngx-spinner';
import { Spinnertype } from 'src/app/base/base.component';
import {
  DeleteDialogComponent,
  DeleteState,
} from 'src/app/dialogs/delete-dialog/delete-dialog.component';
import {
  AlertifyService,
  MessageType,
  Position,
} from 'src/app/services/admin/alertify.service';
import { DialogService } from 'src/app/services/common/dialog.service';
import { HttpClientService } from 'src/app/services/common/http-client.service';

declare var $: any;

//*****DOM nesnelerin manipüle etmek ve evrenselde kullanmak için Directive kullanılır

@Directive({
  selector: '[appDelete]',
})
export class DeleteDirective {
  //Nerede kullanılıyorsa orada declare edilir. Burada products module declare edildi app module değil
  //directive çağırdığımız html nesnesine ihtiyacımız olabilir ElementRef ile elde edilir
  // bunu render edebilmek veya manipüle edebilmek için Renderer2 kullanılır
  //ait id yi sildirebilmek için product Service e ihtiyac var ** product serviside sonradan değiştirdik çünkü evrensel olmalı httpClientSErvice
  //img ı verebilmek için const'ta işlem aypıyoruz
  constructor(
    private element: ElementRef,
    private _renderer: Renderer2,
    private httpClientService: HttpClientService,
    private spinner: NgxSpinnerService,
    public dialog: MatDialog,
    private alertifyService: AlertifyService,
    private dialogService :DialogService
  ) {
    const img = _renderer.createElement('img');
    //img'ın bu derinliği değişkenlik gösterebilir o yüzden refactor edilecek
    img.setAttribute('src', './../../../../assets/delete.png');
    img.setAttribute('style', 'cursor:pointer');
    img.width = 25;
    img.height = 25;
    _renderer.appendChild(element.nativeElement, img);
  }

  //controller değişken olacağından dışardan çağrılcak şekilde düzenlenmelidir
  @Input() controller: string;
  @Input() id: string;
  //list htmlde ki td deki callback i burada yakalıyoruz(angular emitter angular core dan gelmeli!!)
  @Output() callback: EventEmitter<any> = new EventEmitter();
  //td'ye tıklanıldığında (öyle düşünrüsek) silme olacağı için
  //HostListenerda click ' i tetiklediğimiz için tıklanıldığında işlem yapacak
  @HostListener('click')
  async onclick() {
    //yes ise işlem yapacak
    this.dialogService.openDialog({
      componentType:DeleteDialogComponent,
      data:DeleteState.Yes,
      afterClosed() {
        async () => {
          this.spinner.show(Spinnertype.ballScaleMultiple);
          //şimdi ihtiyacımız olan tr ye ulaşmak
          const td: HTMLTableCellElement = this.element.nativeElement;
          //serverdan silme işlemini serviste yapıyoruz servisten burayı çağırcaz
          //burda id alacağımız çin html de td ye id attribute ünü ekliyoruz buradaki id yi @Input ile yakalıyoruz
          //await this.productService.delete(this.id);
          this.httpClientService
            .delete(
              {
                controller: this.controller,
              },
              this.id
            )
            .subscribe(
              (data) => {
                $(td.parentElement).animate(
                  {
                    opacity: 0,
                    left: '+50',
                    height: 'toogle',
                  },
                  700,
                  () => {
                    this.callback.emit();
                    this.alertifyService.message('ürün başarıyla silinmiştir', {
                      dismissOthers: true,
                      messageType: MessageType.Success,
                      position: Position.BottomCenter,
                    });
                  }
                );
              },
              (errorResponse: HttpErrorResponse) => {
                this.spinner.hide(Spinnertype.ballScaleMultiple);
                this.alertifyService.message('ürün silinirken hata oluştu', {
                  dismissOthers: true,
                  messageType: MessageType.Error,
                  position: Position.BottomCenter,
                });
              }
            );
        }
      },
    });
  }

  //Silme işlemi yaparken soru sorsun
  //Dialog component oluşturuyoruz
  //callback yazıyoruz afterClosed adında. çünkü mantıken ilk önce sorsun sonra yukardaki silme işlemini yapsın
  // openDialog(afterClosed: any): void {
  //   const dialogRef = this.dialog.open(DeleteDialogComponent, {
  //     width: '250px',
  //     //dialog açıldığında yes bilgisi gelsin
  //     data: DeleteState.Yes,
  //   });

  //   dialogRef.afterClosed().subscribe((result) => {
  //     if (result == DeleteState.Yes) {
  //       afterClosed();
  //     }
  //   });
  // }
}
