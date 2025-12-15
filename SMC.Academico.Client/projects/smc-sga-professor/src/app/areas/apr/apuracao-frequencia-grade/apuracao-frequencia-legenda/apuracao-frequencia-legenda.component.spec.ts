import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApuracaoFrequenciaLegendaComponent } from './apuracao-frequencia-legenda.component';

describe('ApuracaoFrequenciaLegendaComponent', () => {
  let component: ApuracaoFrequenciaLegendaComponent;
  let fixture: ComponentFixture<ApuracaoFrequenciaLegendaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ApuracaoFrequenciaLegendaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ApuracaoFrequenciaLegendaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
