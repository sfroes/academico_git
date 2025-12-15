import { TestBed } from '@angular/core/testing';

import { SmcLookupTurmaService } from './smc-lookup-turma.service';

describe('SmcLookupTurmaService', () => {
  let service: SmcLookupTurmaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SmcLookupTurmaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
