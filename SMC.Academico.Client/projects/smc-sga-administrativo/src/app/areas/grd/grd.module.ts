import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EventoAulaModule } from './evento-aula/evento-aula.module';
import { GradeHorarioCompartilhadaModule } from './grade-horario-compartilhada/grade-horario-compartilhada.module';

@NgModule({
  imports: [CommonModule, EventoAulaModule, GradeHorarioCompartilhadaModule],
})
export class GrdModule {}
