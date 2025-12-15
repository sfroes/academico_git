import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SmcLookupCicloLetivoComponent } from './smc-lookup-ciclo-letivo.component';

describe('SmcLookupCicloLetivoComponent', () => {
  let component: SmcLookupCicloLetivoComponent;
  let fixture: ComponentFixture<SmcLookupCicloLetivoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SmcLookupCicloLetivoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SmcLookupCicloLetivoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
