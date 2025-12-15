import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EventoAulaAgendamentoEditLocalComponent } from './evento-aula-agendamento-edit-local.component';

describe('EventoAulaAgendamentoEditLocalComponent', () => {
  let component: EventoAulaAgendamentoEditLocalComponent;
  let fixture: ComponentFixture<EventoAulaAgendamentoEditLocalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EventoAulaAgendamentoEditLocalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EventoAulaAgendamentoEditLocalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
