import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EventoAulaAgendamentoEditColaboradorComponent } from './evento-aula-agendamento-edit-colaborador.component';

describe('EventoAulaAgendamentoEditColaboradorComponent', () => {
  let component: EventoAulaAgendamentoEditColaboradorComponent;
  let fixture: ComponentFixture<EventoAulaAgendamentoEditColaboradorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EventoAulaAgendamentoEditColaboradorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EventoAulaAgendamentoEditColaboradorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
