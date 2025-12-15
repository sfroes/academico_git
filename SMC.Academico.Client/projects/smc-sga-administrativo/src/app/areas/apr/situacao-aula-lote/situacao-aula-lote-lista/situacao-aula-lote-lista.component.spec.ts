import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SituacaoAulaLoteListaComponent } from './situacao-aula-lote-lista.component';

describe('SituacaoAulaLoteListaComponent', () => {
  let component: SituacaoAulaLoteListaComponent;
  let fixture: ComponentFixture<SituacaoAulaLoteListaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SituacaoAulaLoteListaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SituacaoAulaLoteListaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
