import { Token } from '@angular/compiler';
import { Injectable } from '@angular/core';
import { firstValueFrom, lastValueFrom, observable, Observable } from 'rxjs';
import { TokenResponse } from 'src/app/contracts/token/tokenResponse';
import { Create_User } from 'src/app/contracts/users/create_user';
import { User } from 'src/app/entities/user';
import {
  CustomToastrService,
  ToastrMessageType,
  ToastrPosition,
} from '../../ui/custom-toastr.service';
import { HttpClientService } from '../http-client.service';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(
    private httpClientService: HttpClientService,
    private toastrService: CustomToastrService
  ) {}

  async create(user: User): Promise<Create_User> {
    const data: Observable<Create_User | User> = this.httpClientService.post(
      {
        controller: 'users',
      },
      user
    );

    //illkali create user model gelmesini isteiğimiz için as Create_User yazıldı
    return (await firstValueFrom(data)) as Create_User;
  }

  //token nesnesi döneceği için karşılayacak contract ı oluşturuyoruz token.ts sınıfında
  async login(
    UsernameOrEmail: string,
    Password: string,
    callBackFunc?: () => void
  ): Promise<any> {
    const data: Observable<any | TokenResponse> = this.httpClientService.post<
      any | TokenResponse
    >(
      {
        controller: 'users',
        action: 'login',
      },
      { UsernameOrEmail, Password }
    );
    //  const token : any  = await firstValueFrom(observable) as Token ;
    const tokenResponse = await firstValueFrom(data);
    if (tokenResponse) {
      //localStorage da tokenı döndürebilmek için tokenResponse contract ı oluşturduk
      localStorage.setItem('accessToken', tokenResponse.token.accessToken);
      this.toastrService.message(
        'Kullanıcı girişi başarıyla sağlanmıştır',
        'Giriş Başarılı',
        {
          messageType: ToastrMessageType.Success,
          position: ToastrPosition.TopFullWidth,
        }
      );
    }
    callBackFunc();
  }
}
