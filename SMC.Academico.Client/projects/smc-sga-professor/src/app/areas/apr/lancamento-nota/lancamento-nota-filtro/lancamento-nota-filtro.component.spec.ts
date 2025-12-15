import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LancamentoNotaFiltroComponent } from './lancamento-nota-filtro.component';

describe('LancamentoNotaFiltroComponent', () => {
  let component: LancamentoNotaFiltroComponent;
  let fixture: ComponentFixture<LancamentoNotaFiltroComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LancamentoNotaFiltroComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LancamentoNotaFiltroComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
