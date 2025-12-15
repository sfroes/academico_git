import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LancamentoNotaCabecalhoComponent } from './lancamento-nota-cabecalho.component';

describe('LancamentoNotaCabecalhoComponent', () => {
  let component: LancamentoNotaCabecalhoComponent;
  let fixture: ComponentFixture<LancamentoNotaCabecalhoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LancamentoNotaCabecalhoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LancamentoNotaCabecalhoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
