import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SmcTableComponent } from './smc-table.component';

describe('SmcTableComponent', () => {
  let component: SmcTableComponent;
  let fixture: ComponentFixture<SmcTableComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SmcTableComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SmcTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
