import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApuracaoFrequenciaGridHeaderAulaComponent } from './apuracao-frequencia-grid-header-aula.component';

describe('ApuracaoFrequenciaGridHeaderAulaComponent', () => {
  let component: ApuracaoFrequenciaGridHeaderAulaComponent;
  let fixture: ComponentFixture<ApuracaoFrequenciaGridHeaderAulaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ApuracaoFrequenciaGridHeaderAulaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ApuracaoFrequenciaGridHeaderAulaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
