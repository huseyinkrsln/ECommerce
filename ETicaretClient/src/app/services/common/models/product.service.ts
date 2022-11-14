import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Create_Product } from 'src/app/contracts/create_product';
import { List_Product } from 'src/app/contracts/list_product';
import { HttpClientService } from '../http-client.service';
import { firstValueFrom, lastValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  constructor(private httpClientService: HttpClientService) {}

  products: List_Product[];

  create(
    product: Create_Product,
    successCallBack?: () => void,
    errorCallBack?: (errorMessage: string) => void
  ) {
    this.httpClientService
      .post(
        {
          controller: 'products',
        },
        product
      )
      .subscribe(
        (result) => {
          successCallBack();
        }
        // (errorResponse: HttpErrorResponse) => {
        //   const _error: Array<{ key: string; value: Array<string> }> =
        //     errorResponse.error;
        //   let message = '';
        //   _error.forEach((v, index) => {
        //     v.value.forEach((_v, _index) => {
        //       message += `${_v}<br>`;
        //     });
        //   });
        //   errorCallBack(message);
        //}
      );
  }

  //paginationda tüm verileri sayfaya yüklememeliyiz. Ne kadarlık ver istiyorsak ekrana o kadar çıkmalı.

     async read(
       page: number = 0,
       size: number = 5,
       successCallBack?: () => void,
       errorCallBack?: (errorMessage: string) => void
     ): Promise<{ totalCount: number; products: List_Product[] }> {
       const promiseData: Promise<{
         totalCount: number;
         products: List_Product[];
       }> = firstValueFrom(
         this.httpClientService.get<{
           totalCount: number;
           products: List_Product[];
         }>({
           controller: 'products',
           queryString: `page=${page}&size=${size}`,
         })
     );

      //   async read (
      //     page: number = 0,
      //     size: number = 5,
      //     successCallBack?: () => void,
      //     errorCallBack?: (errorMessage: string) => void
      //   ): Promise<{ totalCount: number; products: List_Product[] }> {
      //     let promiseData:{
      //       totalCount: number;
      //       products: List_Product[];
      //     } = null;
      //    const getFunc  =
      //       this.httpClientService.get<{
      //         totalCount: number;
      //         products: List_Product[];
      //       }>({
      //         controller: 'products',
      //         queryString: `page=${page}&size=${size}`,
      //       });
      //  await lastValueFrom<{products:List_Product[],totalCount: number}>(getFunc)
      //  .then(data => {promiseData=data;successCallBack()})
      //  .catch((errorResponse: HttpErrorResponse)=>
      //  errorCallBack(errorResponse.message)
      //  )

        promiseData
          .then((d) => successCallBack())
          .catch((errorResponse: HttpErrorResponse) =>
            errorCallBack(errorResponse.message)
          );

     return await  promiseData;
  }
}
