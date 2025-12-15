import { TestBed } from '@angular/core/testing';

import { SmcLookupCursoOfertaLocalidadeService } from './smc-lookup-curso-oferta-localidade.service';

describe('SmcLookupCursoOfertaLocalidadeService', () => {
  let service: SmcLookupCursoOfertaLocalidadeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SmcLookupCursoOfertaLocalidadeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
