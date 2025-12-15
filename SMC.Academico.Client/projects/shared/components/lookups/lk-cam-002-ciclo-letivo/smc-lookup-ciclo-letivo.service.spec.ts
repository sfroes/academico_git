import { TestBed } from '@angular/core/testing';

import { SmcLookupCicloLetivoService } from './smc-lookup-ciclo-letivo.service';

describe('SmcLookupCicloLetivoService', () => {
  let service: SmcLookupCicloLetivoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SmcLookupCicloLetivoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
