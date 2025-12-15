import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GradeHorarioCompartilhadaComponent } from './grade-horario-compartilhada.component';

describe('GradeHorarioCompartilhadaComponent', () => {
  let component: GradeHorarioCompartilhadaComponent;
  let fixture: ComponentFixture<GradeHorarioCompartilhadaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GradeHorarioCompartilhadaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GradeHorarioCompartilhadaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
