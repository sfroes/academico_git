import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EventoAulaAgendamentoAssociarProfessorComponent } from './evento-aula-agendamento-associar-professor.component';

describe('EventoAulaAgendamentoAssociarProfessorComponent', () => {
  let component: EventoAulaAgendamentoAssociarProfessorComponent;
  let fixture: ComponentFixture<EventoAulaAgendamentoAssociarProfessorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EventoAulaAgendamentoAssociarProfessorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EventoAulaAgendamentoAssociarProfessorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
