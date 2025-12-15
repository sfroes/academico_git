import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EventoAulaDivisaoTurmaComponent } from './evento-aula-divisao-turma.component';

describe('EventoAulaDivisaoTurmaComponent', () => {
  let component: EventoAulaDivisaoTurmaComponent;
  let fixture: ComponentFixture<EventoAulaDivisaoTurmaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EventoAulaDivisaoTurmaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EventoAulaDivisaoTurmaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
