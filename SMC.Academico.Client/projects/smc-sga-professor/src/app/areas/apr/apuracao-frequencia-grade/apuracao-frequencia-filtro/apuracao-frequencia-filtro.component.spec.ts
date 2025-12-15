import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApuracaoFrequenciaFiltroComponent } from './apuracao-frequencia-filtro.component';

describe('ApuracaoFrequenciaFiltroComponent', () => {
  let component: ApuracaoFrequenciaFiltroComponent;
  let fixture: ComponentFixture<ApuracaoFrequenciaFiltroComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ApuracaoFrequenciaFiltroComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ApuracaoFrequenciaFiltroComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
