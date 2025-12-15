import { TestBed } from '@angular/core/testing';

import { LancamentoNotaResolverService } from './lancamento-nota-resolver.service';

describe('LancamentoNotaResolverService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: LancamentoNotaResolverService = TestBed.get(LancamentoNotaResolverService);
    expect(service).toBeTruthy();
  });
});
