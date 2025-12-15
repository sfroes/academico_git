import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApuracaoFrequenciaGridItemApuracaoComponent } from './apuracao-frequencia-grid-item-apuracao.component';

describe('ApuracaoFrequenciaGridItemApuracaoComponent', () => {
  let component: ApuracaoFrequenciaGridItemApuracaoComponent;
  let fixture: ComponentFixture<ApuracaoFrequenciaGridItemApuracaoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ApuracaoFrequenciaGridItemApuracaoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ApuracaoFrequenciaGridItemApuracaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
