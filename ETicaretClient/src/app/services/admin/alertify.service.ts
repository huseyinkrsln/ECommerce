import { Injectable } from '@angular/core';

declare var alertify: any;

@Injectable({
  providedIn: 'root',
})
export class AlertifyService {
  constructor() {}

  // message(message: string, messageType: MessageType, position: Position,delay:number=3,dismissOthers:boolean = false)
    //partial yazmamızım sebibi parametrede kullanırken {} yazarak rahtça kullanabiliriz
  message(message:string,options:Partial<AlertifyOptions>) {
    alertify.set('notifier', 'position', options.position);
    alertify.set('notifier','delay', 10);
    const msj = alertify[options.messageType](message);
    if(options.dismissOthers)
      msj.dismissOthers();
  }

dismissAll(){
  alertify.dismissAll();
}

}

export class AlertifyOptions{
  messageType:MessageType = MessageType.Message;
  position:Position=Position.BottomCenter;
  delay:number =3;
  dismissOthers:boolean = false;

}

export enum MessageType {
  Error = 'error',
  Message = 'message',
  Notify = 'notify',
  Success = 'success',
  Warning = 'warning',
}

export enum Position {
  TopCenter = 'top-center',
  TopRight = 'top-right',
  TopLeft = 'top-left',
  BottomRight = 'bottom-right',
  BottomLeft = 'bottom-left',
  BottomCenter = 'bottom-center',
}
