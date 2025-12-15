import { TestBed } from '@angular/core/testing';

import { SituacaoAulaLoteService } from './situacao-aula-lote.service';

describe('SituacaoAulaLoteService', () => {
  let service: SituacaoAulaLoteService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SituacaoAulaLoteService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
