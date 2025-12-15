import { TestBed } from '@angular/core/testing';

import { ApuracaoFrequenciaDataService } from './apuracao-frequencia-data.service';

describe('ApuracaoFrequenciaDataService', () => {
  let service: ApuracaoFrequenciaDataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ApuracaoFrequenciaDataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
