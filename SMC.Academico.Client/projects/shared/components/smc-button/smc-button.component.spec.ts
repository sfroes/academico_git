import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SmcButtonComponent } from './smc-button.component';

describe('SmcButtonComponent', () => {
  let component: SmcButtonComponent;
  let fixture: ComponentFixture<SmcButtonComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SmcButtonComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SmcButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
