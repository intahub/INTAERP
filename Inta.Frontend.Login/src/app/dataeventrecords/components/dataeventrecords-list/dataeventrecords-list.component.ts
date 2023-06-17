import { Component, OnInit } from '@angular/core';
import { switchMap } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { OidcSecurityService, AuthenticatedResult } from 'angular-auth-oidc-client';

import { DataeventrecordsService } from '../../dataeventrecords.service';
import { DataEventRecord } from '../../models/DataEventRecords';

@Component({
  selector: 'app-dataeventrecords-list',
  templateUrl: './dataeventrecords-list.component.html',
  styleUrls: ['./dataeventrecords-list.component.css']
})
export class DataeventrecordsListComponent implements OnInit{
  message: string;
  DataEventRecords: DataEventRecord[] = [];
  hasAdminRole = false;
  isAuthenticated$: Observable<AuthenticatedResult>;

    constructor(

      private dataEventRecordsService: DataeventrecordsService,
      public oidcSecurityService: OidcSecurityService,
    ) {
        debugger
        this.message = 'DataEventRecords';
        this.isAuthenticated$ = this.oidcSecurityService.isAuthenticated$;
    }

    ngOnInit() {
        this.isAuthenticated$.pipe(
            switchMap(({ isAuthenticated }) => this.getData(isAuthenticated))
        ).subscribe(
            data => this.DataEventRecords = data,
            () => console.log('getData Get all completed')
        );

        this.oidcSecurityService.userData$.subscribe(({userData}) => {
            console.log('Get userData: ', userData);
            if (userData) {
                console.log('userData: ', userData);

                if (userData !== '' && userData.role) {
                    for (let i = 0; i < userData.role.length; i++) {
                        if (userData.role[i] === 'dataEventRecords.admin') {
                            this.hasAdminRole = true;
                        }
                        if (userData.role[i] === 'admin') {
                        }
                    }
                }
            }
        });
    }

    private getData(isAuthenticated: boolean): Observable<DataEventRecord[]> {
      if (isAuthenticated) {
          return this.dataEventRecordsService.GetAll();
      }
      return of([]);
  }
}
