import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable } from 'rxjs';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { DataEventRecord } from './models/DataEventRecords';

@Injectable({
  providedIn: 'root'
})
export class DataeventrecordsService {

  private actionUrl: string;
  private headers: HttpHeaders = new HttpHeaders();

  constructor(private http: HttpClient, private securityService: OidcSecurityService) {
      this.actionUrl = `https://localhost:44390/api/DataEventRecords/`;
  }

  private setHeaders(): any {
    this.headers = new HttpHeaders();
    this.headers = this.headers.set('Content-Type', 'application/json');
    this.headers = this.headers.set('Accept', 'application/json');

    this.securityService.getAccessToken().subscribe((token) => {
      if (token !== '') {
        const tokenValue = 'Bearer ' + token;
        this.headers = this.headers.append('Authorization', tokenValue);
      }
    });
  }

  public GetAll = (): Observable<DataEventRecord[]> => {
    this.setHeaders();

    return this.http.get<DataEventRecord[]>(this.actionUrl, { headers: this.headers });
}
}
