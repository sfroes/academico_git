import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApuracaoFrequenciaGridHeaderComponent } from './apuracao-frequencia-grid-header.component';

describe('ApuracaoFrequenciaGridHeaderComponent', () => {
  let component: ApuracaoFrequenciaGridHeaderComponent;
  let fixture: ComponentFixture<ApuracaoFrequenciaGridHeaderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ApuracaoFrequenciaGridHeaderComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ApuracaoFrequenciaGridHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
