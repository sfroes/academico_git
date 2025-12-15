import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EventoAulaAgendamentoDeleteComponent } from './evento-aula-agendamento-delete.component';

describe('EventoAulaAgendamentoDeleteComponent', () => {
  let component: EventoAulaAgendamentoDeleteComponent;
  let fixture: ComponentFixture<EventoAulaAgendamentoDeleteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EventoAulaAgendamentoDeleteComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EventoAulaAgendamentoDeleteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
