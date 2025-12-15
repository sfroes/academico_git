import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EventoAulaAgendamentoEditColaboradorSubstitutoComponent } from './evento-aula-agendamento-edit-colaborador-substituto.component';

describe('EventoAulaAgendamentoEditColaboradorComponent', () => {
  let component: EventoAulaAgendamentoEditColaboradorSubstitutoComponent;
  let fixture: ComponentFixture<EventoAulaAgendamentoEditColaboradorSubstitutoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [EventoAulaAgendamentoEditColaboradorSubstitutoComponent],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EventoAulaAgendamentoEditColaboradorSubstitutoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
