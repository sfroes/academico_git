import { TestBed } from '@angular/core/testing';

import { LancamentoNotaService } from './lancamento-nota.service';

describe('LancamentoNotaService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: LancamentoNotaService = TestBed.get(LancamentoNotaService);
    expect(service).toBeTruthy();
  });
});
