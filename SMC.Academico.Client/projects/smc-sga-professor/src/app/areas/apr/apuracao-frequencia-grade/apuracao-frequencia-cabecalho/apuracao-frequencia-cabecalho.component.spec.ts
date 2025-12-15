import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApuracaoFrequenciaCabecalhoComponent } from './apuracao-frequencia-cabecalho.component';

describe('ApuracaoFrequenciaCabecalhoComponent', () => {
  let component: ApuracaoFrequenciaCabecalhoComponent;
  let fixture: ComponentFixture<ApuracaoFrequenciaCabecalhoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ApuracaoFrequenciaCabecalhoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ApuracaoFrequenciaCabecalhoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
