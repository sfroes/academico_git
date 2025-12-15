import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SmcAssertComponent } from './smc-assert.component';

describe('SmcAssertComponent', () => {
  let component: SmcAssertComponent;
  let fixture: ComponentFixture<SmcAssertComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SmcAssertComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SmcAssertComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
