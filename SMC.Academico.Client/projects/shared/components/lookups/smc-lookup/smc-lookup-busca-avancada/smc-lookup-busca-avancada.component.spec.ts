import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SmcLookupBuscaAvancadaComponent } from './smc-lookup-busca-avancada.component';

describe('SmcLookupBuscaAvancadaComponent', () => {
  let component: SmcLookupBuscaAvancadaComponent;
  let fixture: ComponentFixture<SmcLookupBuscaAvancadaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SmcLookupBuscaAvancadaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SmcLookupBuscaAvancadaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
