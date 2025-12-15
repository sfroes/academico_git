import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SmcLookupColaboradorComponent } from './smc-lookup-colaborador.component';

describe('SmcLookupColaboradorComponent', () => {
  let component: SmcLookupColaboradorComponent;
  let fixture: ComponentFixture<SmcLookupColaboradorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SmcLookupColaboradorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SmcLookupColaboradorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
