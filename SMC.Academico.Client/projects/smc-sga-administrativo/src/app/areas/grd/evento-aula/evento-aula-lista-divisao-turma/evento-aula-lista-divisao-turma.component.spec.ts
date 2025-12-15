import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EventoAulaListaDivisaoTurmaComponent } from './evento-aula-lista-divisao-turma.component';

describe('EventoAulaListaDivisaoTurmaComponent', () => {
  let component: EventoAulaListaDivisaoTurmaComponent;
  let fixture: ComponentFixture<EventoAulaListaDivisaoTurmaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EventoAulaListaDivisaoTurmaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EventoAulaListaDivisaoTurmaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
