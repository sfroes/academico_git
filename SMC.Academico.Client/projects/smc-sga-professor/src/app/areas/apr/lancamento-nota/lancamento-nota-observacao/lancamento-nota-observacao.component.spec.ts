import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LancamentoNotaObservacaoComponent } from './lancamento-nota-observacao.component';

describe('LancamentoNotaObservacaoComponent', () => {
  let component: LancamentoNotaObservacaoComponent;
  let fixture: ComponentFixture<LancamentoNotaObservacaoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LancamentoNotaObservacaoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LancamentoNotaObservacaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
