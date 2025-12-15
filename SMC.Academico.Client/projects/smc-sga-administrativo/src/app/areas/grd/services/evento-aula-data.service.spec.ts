import { TestBed } from '@angular/core/testing';

import { EventoAulaDataService } from './evento-aula-data.service';

describe('EventoAulaDataService', () => {
  let service: EventoAulaDataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EventoAulaDataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
