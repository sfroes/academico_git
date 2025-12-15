import { TestBed } from '@angular/core/testing';

import { SmcLookupColaboradorService } from './smc-lookup-colaborador.service';

describe('SmcLookupColaboradorService', () => {
  let service: SmcLookupColaboradorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SmcLookupColaboradorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
