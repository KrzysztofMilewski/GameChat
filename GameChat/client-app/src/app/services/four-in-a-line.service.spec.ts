import { TestBed } from '@angular/core/testing';

import { FourInALineService } from './four-in-a-line.service';

describe('FourInALineService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: FourInALineService = TestBed.get(FourInALineService);
    expect(service).toBeTruthy();
  });
});
