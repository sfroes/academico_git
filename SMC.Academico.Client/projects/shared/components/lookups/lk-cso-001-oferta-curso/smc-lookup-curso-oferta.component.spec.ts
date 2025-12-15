import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SmcLookupCursoOfertaComponent } from './smc-lookup-curso-oferta.component';

describe('SmcLookupCursoOfertaComponent', () => {
  let component: SmcLookupCursoOfertaComponent;
  let fixture: ComponentFixture<SmcLookupCursoOfertaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SmcLookupCursoOfertaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SmcLookupCursoOfertaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
