import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EventoAulaAgendamentoSimulacaoComponent } from './evento-aula-agendamento-Simulacao.component';

describe('EventoAulaAgendamentoSimulacaoComponent', () => {
  let component: EventoAulaAgendamentoSimulacaoComponent;
  let fixture: ComponentFixture<EventoAulaAgendamentoSimulacaoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EventoAulaAgendamentoSimulacaoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EventoAulaAgendamentoSimulacaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
