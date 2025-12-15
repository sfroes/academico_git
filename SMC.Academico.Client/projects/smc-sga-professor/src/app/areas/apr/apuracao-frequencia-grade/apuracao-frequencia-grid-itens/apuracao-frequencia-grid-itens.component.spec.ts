import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApuracaoFrequenciaGridItensComponent } from './apuracao-frequencia-grid-itens.component';

describe('ApuracaoFrequenciaGridItensComponent', () => {
  let component: ApuracaoFrequenciaGridItensComponent;
  let fixture: ComponentFixture<ApuracaoFrequenciaGridItensComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ApuracaoFrequenciaGridItensComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ApuracaoFrequenciaGridItensComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
