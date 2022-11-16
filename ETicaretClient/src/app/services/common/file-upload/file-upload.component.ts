import { HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import {
  NgxFileDropEntry,
  FileSystemFileEntry,
  FileSystemDirectoryEntry,
} from 'ngx-file-drop';
import {
  FileUploadDialogComponent,
  FileUploadDialogState,
} from 'src/app/dialogs/file-upload-dialog/file-upload-dialog.component';
import {
  AlertifyService,
  MessageType,
  Position,
} from '../../admin/alertify.service';
import {
  CustomToastrService,
  ToastrMessageType,
  ToastrPosition,
} from '../../ui/custom-toastr.service';
import { DialogService } from '../dialog.service';
import { HttpClientService } from '../http-client.service';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.scss'],
})
//npm ngx-file-upload modülü indirildi
//App module'e oto declare oldu fakat bunu kaldırmalıyız çünkü dileUpload modulümüz var onadeclare olmalı
export class FileUploadComponent {
  constructor(
    private alertifyService: AlertifyService,
    private customToastrService: CustomToastrService,
    private httpClientService: HttpClientService,
    private dialog: MatDialog,
    private dialogService : DialogService,
  ) {}

  //default [] alıyordu kaldırdım çünkü html de doys seçilemdiği zaan seçilenler kısmı gözükmsini istemiyorum
  public files: NgxFileDropEntry[];

  //Değerleri yakalayabilmek için Partial çünkü obje oluşturduk ulaşımı rahat olsun
  //ilgili creaete html de bildiriyoruz
  @Input() options: Partial<FileUploadOptions>;

  public selectedFiles(files: NgxFileDropEntry[]) {
    this.files = files;
    //req yapılacak dosyaların olacağı form data olacak
    const fileData: FormData = new FormData();
    const messageValid: string = 'Dosyalar başarıyle yüklenmiştir';
    const messageFailed: string =
      'Dosyalar yükleme yapılırken hatayla karşılaşılmıştır';

    for (const file of files) {
      const fileEntry = file.fileEntry as FileSystemFileEntry;
      fileEntry.file((_file: File) => {
        fileData.append(_file.name, _file, file.relativePath);
      });
    }

    this.dialogService.openDialog({
      componentType:FileUploadDialogComponent,
      data:FileUploadDialogState.Yes,
      afterClosed() {
        () => {
          this.httpClientService
            .post(
              {
                controller: this.options.controller,
                action: this.options.action,
                queryString: this.options.queryString,
                headers: new HttpHeaders({ responseType: 'blob' }),
              },
              fileData
            )
            .subscribe(
              (data) => {
                if (this.options.isAdminPanel) {
                  this.alertifyService.message(messageValid, {
                    dismissOthers: true,
                    messageType: MessageType.Success,
                    position: Position.TopRight,
                  });
                } else {
                  this.customToastrService.message(messageValid, 'Başarılı', {
                    messageType: ToastrMessageType.Success,
                    position: ToastrPosition.TopRight,
                  });
                }
              },
              (errorResponse: HttpErrorResponse) => {
                if (this.options.isAdminPanel) {
                  this.alertifyService.message(messageFailed, {
                    dismissOthers: true,
                    messageType: MessageType.Error,
                    position: Position.TopRight,
                  });
                } else {
                  this.customToastrService.message(messageFailed, 'Başarısız', {
                    messageType: ToastrMessageType.Error,
                    position: ToastrPosition.TopRight,
                  });
                }
              }
            );
        }
      },

    });
  }

  // openDialog(afterClosed: any): void {
  //   const dialogRef = this.dialog.open(FileUploadDialogComponent, {
  //     width: '250px',
  //     //dialog açıldığında yes bilgisi gelsin
  //     data: FileUploadDialogState.Yes,
  //   });

  //   dialogRef.afterClosed().subscribe((result) => {
  //     if (result == FileUploadDialogState.Yes) {
  //       afterClosed();
  //     }
  //   });
 // }
}
export class FileUploadOptions {
  controller?: string;
  action?: string;
  queryString?: string;
  explanation?: string;
  accept?: string;
  //alertler farklı old için admin ve ui da fakı belli etmek için yazıldı
  isAdminPanel?: boolean = false;
}
