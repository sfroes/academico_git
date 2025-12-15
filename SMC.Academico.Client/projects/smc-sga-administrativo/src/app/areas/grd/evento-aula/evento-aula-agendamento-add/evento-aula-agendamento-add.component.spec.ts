import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EventoAulaAgendamentoAddComponent } from './evento-aula-agendamento-add.component';

describe('EventoAulaAgendamentoAddComponent', () => {
  let component: EventoAulaAgendamentoAddComponent;
  let fixture: ComponentFixture<EventoAulaAgendamentoAddComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EventoAulaAgendamentoAddComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EventoAulaAgendamentoAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
