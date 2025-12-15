import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EventoAulaNotificacoesComponent } from './evento-aula-notificacoes.component';

describe('EventoAulaNotificacoesComponent', () => {
  let component: EnventoAulaNotificacoesComponent;
  let fixture: ComponentFixture<EventoAulaNotificacoesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EnventoAulaNotificacoesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EventoAulaNotificacoesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
