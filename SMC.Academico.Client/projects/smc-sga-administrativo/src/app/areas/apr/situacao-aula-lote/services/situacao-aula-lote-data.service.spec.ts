import { TestBed } from '@angular/core/testing';

import { SituacaoAulaLoteDataService } from './situacao-aula-lote-data.service';

describe('SituacaoAulaLoteDataService', () => {
  let service: SituacaoAulaLoteDataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SituacaoAulaLoteDataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
