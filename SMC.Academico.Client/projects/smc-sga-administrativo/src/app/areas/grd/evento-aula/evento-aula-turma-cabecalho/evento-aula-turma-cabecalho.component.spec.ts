import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EventoAulaTurmaCabecalhoComponent } from './evento-aula-turma-cabecalho.component';

describe('EventoAulaTurmaCabecalhoComponent', () => {
  let component: EventoAulaTurmaCabecalhoComponent;
  let fixture: ComponentFixture<EventoAulaTurmaCabecalhoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EventoAulaTurmaCabecalhoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EventoAulaTurmaCabecalhoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
