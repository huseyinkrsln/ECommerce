import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class HttpClientService {
  constructor(
    private httpClient: HttpClient,
    @Inject('baseUrl') private baseUrl: string
  ) {}

  // tüm isteklerde yapılacak url yapılanması
  private url(requestParameter: Partial<RequestParams>): string {
    return ` ${
      requestParameter.baseUrl ? requestParameter.baseUrl : this.baseUrl
    }/${requestParameter.controller}${
      requestParameter.action ? `/${requestParameter.action}` : ''
    }`;
  }

  get<T>(requestParameter: Partial<RequestParams>, id?: string): Observable<T> {
    let url: string = '';
    if (requestParameter.fullEndPoint) url = requestParameter.fullEndPoint;
    else url = `${this.url(requestParameter)}${id ? `/${id}` : ''}${requestParameter.queryString ? `?${requestParameter.queryString}`: '' }`;

    return this.httpClient.get<T>(url, { headers: requestParameter.headers });
  }

  post<T>(
    requestParameter: Partial<RequestParams>,
    body: Partial<T>
  ): Observable<T> {
    let url = '';
    if (requestParameter.fullEndPoint) url = requestParameter.fullEndPoint;
    else url = `${this.url(requestParameter)} ${requestParameter.queryString ? `?${requestParameter.queryString}`: '' }`;

    return this.httpClient.post<T>(url, body, {
      headers: requestParameter.headers,
    });
  }

  put<T>(
    requestParameter: Partial<RequestParams>,
    body: Partial<T>
  ): Observable<T> {
    let url: string = '';
    if (requestParameter.fullEndPoint) url = requestParameter.fullEndPoint;
    else url = `${this.url(requestParameter)}${requestParameter.queryString ? `?${requestParameter.queryString}`: '' }`;

    return this.httpClient.put<T>(url, body, {
      headers: requestParameter.headers,
    });
  }

  delete<T>(
    requestParameter: Partial<RequestParams>,
    id: string
  ): Observable<T> {
    let url: string = '';
    if (requestParameter.fullEndPoint) url = requestParameter.fullEndPoint;
    else url = `${this.url(requestParameter)}/${id}${requestParameter.queryString ? `?${requestParameter.queryString}`: '' }`;

    return this.httpClient.delete<T>(url, {
      headers: requestParameter.headers,
    });
  }
}

export class RequestParams {
  controller?: string;
  action?: string;
  queryString?:string;
  headers?: HttpHeaders;
  baseUrl?: string;
  fullEndPoint: string;
}
