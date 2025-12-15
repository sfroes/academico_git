import { TestBed } from '@angular/core/testing';

import { MiniProfilerInterceptor } from './mini-profiler.interceptor';

describe('MiniProfilerInterceptor', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      MiniProfilerInterceptor
      ]
  }));

  it('should be created', () => {
    const interceptor: MiniProfilerInterceptor = TestBed.inject(MiniProfilerInterceptor);
    expect(interceptor).toBeTruthy();
  });
});
