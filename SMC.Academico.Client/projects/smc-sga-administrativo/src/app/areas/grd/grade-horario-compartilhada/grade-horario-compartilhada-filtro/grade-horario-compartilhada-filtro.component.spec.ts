import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GradeHorarioCompartilhadaFiltroComponent } from './grade-horario-compartilhada-filtro.component';

describe('GradeHorarioCompartilhadaFiltroComponent', () => {
  let component: GradeHorarioCompartilhadaFiltroComponent;
  let fixture: ComponentFixture<GradeHorarioCompartilhadaFiltroComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GradeHorarioCompartilhadaFiltroComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GradeHorarioCompartilhadaFiltroComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
