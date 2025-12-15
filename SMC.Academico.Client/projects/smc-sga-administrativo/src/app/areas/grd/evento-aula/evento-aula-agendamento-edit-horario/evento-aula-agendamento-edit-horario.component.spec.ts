import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EventoAulaAgendamentoEditHorarioComponent } from './evento-aula-agendamento-edit-horario.component';

describe('EventoAulaAgendamentoEditHorarioComponent', () => {
  let component: EventoAulaAgendamentoEditHorarioComponent;
  let fixture: ComponentFixture<EventoAulaAgendamentoEditHorarioComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EventoAulaAgendamentoEditHorarioComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EventoAulaAgendamentoEditHorarioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
