import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GradeHorarioCompartilhadaListaComponent } from './grade-horario-compartilhada-lista.component';

describe('GradeHorarioCompartilhadaListaComponent', () => {
  let component: GradeHorarioCompartilhadaListaComponent;
  let fixture: ComponentFixture<GradeHorarioCompartilhadaListaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GradeHorarioCompartilhadaListaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GradeHorarioCompartilhadaListaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
