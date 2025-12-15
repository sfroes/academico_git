import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SmcPagerComponent } from './smc-pager.component';

describe('SmcPagerComponent', () => {
  let component: SmcPagerComponent;
  let fixture: ComponentFixture<SmcPagerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SmcPagerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SmcPagerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
