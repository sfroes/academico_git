import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SmcLookupCursoOfertaLocalidadeComponent } from './smc-lookup-curso-oferta-localidade.component';

describe('SmcLookupCursoOfertaLocalidadeComponent', () => {
  let component: SmcLookupCursoOfertaLocalidadeComponent;
  let fixture: ComponentFixture<SmcLookupCursoOfertaLocalidadeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SmcLookupCursoOfertaLocalidadeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SmcLookupCursoOfertaLocalidadeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
