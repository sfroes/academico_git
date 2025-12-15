import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApuracaoFrequenciaGridComponent } from './apuracao-frequencia-grid.component';

describe('ApuracaoFrequenciaGridComponent', () => {
  let component: ApuracaoFrequenciaGridComponent;
  let fixture: ComponentFixture<ApuracaoFrequenciaGridComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ApuracaoFrequenciaGridComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ApuracaoFrequenciaGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
