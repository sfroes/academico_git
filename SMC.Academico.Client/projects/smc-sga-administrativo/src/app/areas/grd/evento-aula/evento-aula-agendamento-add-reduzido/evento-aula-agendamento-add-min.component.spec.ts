import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EventoAulaAgendamentoAddMinComponent } from './evento-aula-agendamento-add-min.component';

describe('EventoAulaAgendamentoAddMinComponent', () => {
  let component: EventoAulaAgendamentoAddMinComponent;
  let fixture: ComponentFixture<EventoAulaAgendamentoAddMinComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EventoAulaAgendamentoAddMinComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EventoAulaAgendamentoAddMinComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
