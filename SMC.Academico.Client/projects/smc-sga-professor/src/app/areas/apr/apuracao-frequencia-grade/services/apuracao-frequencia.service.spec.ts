import { TestBed } from '@angular/core/testing';

import { ApuracaoFrequenciaService } from './apuracao-frequencia.service';

describe('ApuracaoFrequenciaService', () => {
  let service: ApuracaoFrequenciaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ApuracaoFrequenciaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
