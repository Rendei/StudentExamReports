import { TestBed } from '@angular/core/testing';

import { TownTypeService } from './town-type.service';

describe('TownTypeService', () => {
  let service: TownTypeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TownTypeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
