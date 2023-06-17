import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { DataeventrecordsListComponent } from './components/dataeventrecords-list/dataeventrecords-list.component';

const routes: Routes = [
  {
    path: 'dataeventrecords',
    component: DataeventrecordsListComponent,
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DataeventrecordsRoutingModule { }
