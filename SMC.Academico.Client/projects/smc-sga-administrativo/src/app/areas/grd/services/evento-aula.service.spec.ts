import { TestBed } from '@angular/core/testing';

import { EventoAulaService } from './evento-aula.service';

describe('EventoAulaService', () => {
  let service: EventoAulaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EventoAulaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
