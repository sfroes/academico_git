import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EventoAulaAgendamentoDetalhesComponent } from './evento-aula-agendamento-detalhes.component';

describe('EventoAulaAgendamentoDetalhesComponent', () => {
  let component: EventoAulaAgendamentoDetalhesComponent;
  let fixture: ComponentFixture<EventoAulaAgendamentoDetalhesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [EventoAulaAgendamentoDetalhesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EventoAulaAgendamentoDetalhesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
