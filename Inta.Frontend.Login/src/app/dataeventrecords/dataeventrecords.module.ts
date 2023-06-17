import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { DataeventrecordsRoutingModule } from './dataeventrecords-routing.module';
import { DataeventrecordsListComponent } from './components/dataeventrecords-list/dataeventrecords-list.component';
import { DataeventrecordsService } from './dataeventrecords.service';

@NgModule({
  declarations: [
    DataeventrecordsListComponent
  ],
  imports: [
    CommonModule,
    DataeventrecordsRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [
    DataeventrecordsService
  ],
  exports: [
    DataeventrecordsListComponent
  ]
})
export class DataeventrecordsModule { }
