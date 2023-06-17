import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { FormsModule } from '@angular/forms';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { AuthModule, LogLevel } from 'angular-auth-oidc-client';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { ForbiddenComponent } from './forbidden/forbidden.component';
import { DataeventrecordsModule } from './dataeventrecords/dataeventrecords.module';

@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule,
    DataeventrecordsModule,
    AuthModule.forRoot({
      config: {
          authority: 'https://localhost:44395',
          redirectUrl: window.location.origin,
          postLogoutRedirectUri: window.location.origin,
          clientId: 'Inta_ERP_Angular_Client',
          scope: 'openid profile email dataEventRecords offline_access',
          responseType: 'code',
          silentRenew: true,
          renewTimeBeforeTokenExpiresInSeconds: 10,
          useRefreshToken: true,
          logLevel: LogLevel.Debug,
      },
    }),
  ],
  declarations: [
    AppComponent,
    HomeComponent,
    UnauthorizedComponent,
    ForbiddenComponent
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { 
  constructor() {
    console.log('APP STARTING');
  } 
}
