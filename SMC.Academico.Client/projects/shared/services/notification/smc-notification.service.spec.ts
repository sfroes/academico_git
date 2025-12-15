import { TestBed } from '@angular/core/testing';

import { SmcNotificationService } from './smc-notification.service';

describe('SmcNotificationService', () => {
  let service: SmcNotificationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SmcNotificationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
