import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SmcModalComponent } from './smc-modal.component';

describe('SmcModalComponent', () => {
  let component: SmcModalComponent;
  let fixture: ComponentFixture<SmcModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SmcModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SmcModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
