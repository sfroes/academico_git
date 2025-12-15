import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApuracaoFrequenciaNotificacoesComponent } from './apuracao-frequencia-notificacoes.component';

describe('ApuracaoFrequenciaNotificacoesComponent', () => {
  let component: ApuracaoFrequenciaNotificacoesComponent;
  let fixture: ComponentFixture<ApuracaoFrequenciaNotificacoesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ApuracaoFrequenciaNotificacoesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ApuracaoFrequenciaNotificacoesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
