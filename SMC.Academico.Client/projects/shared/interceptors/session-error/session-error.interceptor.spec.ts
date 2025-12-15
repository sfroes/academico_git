import { TestBed } from '@angular/core/testing';

import { SessionErrorInterceptor } from './session-error.interceptor';

describe('SessionErrorInterceptor', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      SessionErrorInterceptor
      ]
  }));

  it('should be created', () => {
    const interceptor: SessionErrorInterceptor = TestBed.inject(SessionErrorInterceptor);
    expect(interceptor).toBeTruthy();
  });
});
