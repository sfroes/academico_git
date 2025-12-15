import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApuracaoFrequenciaComponent } from './apuracao-frequencia.component';

describe('ApuracaoFrequenciaComponent', () => {
  let component: ApuracaoFrequenciaComponent;
  let fixture: ComponentFixture<ApuracaoFrequenciaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ApuracaoFrequenciaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ApuracaoFrequenciaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
