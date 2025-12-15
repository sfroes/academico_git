import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SmcSelectComponent } from './smc-select.component';

describe('SmcSelectComponent', () => {
  let component: SmcSelectComponent;
  let fixture: ComponentFixture<SmcSelectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SmcSelectComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SmcSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
