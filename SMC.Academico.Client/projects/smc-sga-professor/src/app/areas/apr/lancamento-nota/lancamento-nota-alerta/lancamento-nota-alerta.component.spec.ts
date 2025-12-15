import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LancamentoNotaAlertaComponent } from './lancamento-nota-alerta.component';

describe('LancamentoNotaAlertaComponent', () => {
  let component: LancamentoNotaAlertaComponent;
  let fixture: ComponentFixture<LancamentoNotaAlertaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LancamentoNotaAlertaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LancamentoNotaAlertaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
