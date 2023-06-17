import { TestBed } from '@angular/core/testing';

import { DataeventrecordsService } from './dataeventrecords.service';

describe('DataeventrecordsService', () => {
  let service: DataeventrecordsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DataeventrecordsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
