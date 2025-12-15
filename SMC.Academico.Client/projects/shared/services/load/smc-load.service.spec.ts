import { TestBed } from '@angular/core/testing';

import { SmcLoadService } from './smc-load.service';

describe('SmcLoadService', () => {
  let service: SmcLoadService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SmcLoadService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
