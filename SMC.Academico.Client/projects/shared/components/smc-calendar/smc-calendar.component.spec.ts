import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SmcCalendarComponent } from './smc-calendar.component';

describe('SmcCalendarComponent', () => {
  let component: SmcCalendarComponent;
  let fixture: ComponentFixture<SmcCalendarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SmcCalendarComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SmcCalendarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
