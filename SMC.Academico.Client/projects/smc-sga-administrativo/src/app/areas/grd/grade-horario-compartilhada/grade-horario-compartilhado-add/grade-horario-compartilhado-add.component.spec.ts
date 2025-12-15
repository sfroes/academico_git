import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GradeHorarioCompartilhadoAddComponent } from './grade-horario-compartilhado-add.component';

describe('GradeHorarioCompartilhadoAddComponent', () => {
  let component: GradeHorarioCompartilhadoAddComponent;
  let fixture: ComponentFixture<GradeHorarioCompartilhadoAddComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GradeHorarioCompartilhadoAddComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GradeHorarioCompartilhadoAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
