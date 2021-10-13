import { IUserInterface } from 'src/app/interfaces/iuserInterface';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  constructor(private http: HttpClient) { }

  get(address: string): Observable<any> {
    return this.http.get<any>(environment.URL_API + address);
  }

  post(address: string, body: IUserInterface): Observable<any> {
    return this.http.post<any>(environment.URL_API + address, body);
  }

  postExcel(address: string, body: IUserInterface[]): Observable<any> {
    return this.http.post<any>(environment.URL_API + address, body);
  }

  put(address: string, body: IUserInterface): Observable<any> {
    return this.http.put<any>(environment.URL_API + address, body);
  }

  delete(address: string, id: string): Observable<any> {
    const params = new HttpParams().set('Id', id);
    return this.http.delete<any>(environment.URL_API + address, {params});
  }

  deleteAll(address: string): Observable<any> {
    return this.http.delete<any>(environment.URL_API + address);
  }
}
