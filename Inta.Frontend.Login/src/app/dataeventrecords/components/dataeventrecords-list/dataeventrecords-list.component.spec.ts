import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DataeventrecordsListComponent } from './dataeventrecords-list.component';

describe('DataeventrecordsListComponent', () => {
  let component: DataeventrecordsListComponent;
  let fixture: ComponentFixture<DataeventrecordsListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DataeventrecordsListComponent]
    });
    fixture = TestBed.createComponent(DataeventrecordsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
