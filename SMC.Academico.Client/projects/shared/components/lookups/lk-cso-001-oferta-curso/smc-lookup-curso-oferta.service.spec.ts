import { TestBed } from '@angular/core/testing';

import { SmcLookupCursoOfertaService } from './smc-lookup-curso-oferta.service';

describe('SmcLookupCursoOfertaService', () => {
  let service: SmcLookupCursoOfertaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SmcLookupCursoOfertaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
