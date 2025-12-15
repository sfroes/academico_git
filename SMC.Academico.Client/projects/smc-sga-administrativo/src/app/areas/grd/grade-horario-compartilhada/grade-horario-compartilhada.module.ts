import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GradeHorarioCompartilhadaFiltroComponent } from './grade-horario-compartilhada-filtro/grade-horario-compartilhada-filtro.component';
import { SharedModule } from '../../../shared/shared.module';
import { SmcButtonModule } from 'projects/shared/components/smc-button/smc-button.module';
import { SmcLookupTurmaModule } from 'projects/shared/components/lookups/lk-tur-001-turma/smc-lookup-turma.module';
import { SmcModalModule } from 'projects/shared/components/smc-modal/smc-modal.module';
import { SmcSelectModule } from 'projects/shared/components/smc-select/smc-select.module';
import { SmcLookupColaboradorModule } from 'projects/shared/components/lookups/lk-dct-001-colaborador/smc-lookup-colaborador.module';
import { SmcLookupCicloLetivoModule } from 'projects/shared/components/lookups/lk-cam-002-ciclo-letivo/smc-lookup-ciclo-letivo.module';
import { PoTableModule } from '@po-ui/ng-components';
import { GradeHorarioCompartilhadaComponent } from './grade-horario-compartilhada.component';
import { GradeHorarioCompartilhadaListaComponent } from './grade-horario-compartilhada-lista/grade-horario-compartilhada-lista.component';
import { GradeHorarioCompartilhadoAddComponent } from './grade-horario-compartilhado-add/grade-horario-compartilhado-add.component';

@NgModule({
  declarations: [GradeHorarioCompartilhadaComponent, GradeHorarioCompartilhadaFiltroComponent, GradeHorarioCompartilhadaListaComponent, GradeHorarioCompartilhadoAddComponent],
  imports: [
    SharedModule,
    CommonModule,
    SmcButtonModule,
    SmcLookupTurmaModule,
    SmcModalModule,
    SmcSelectModule,
    SmcLookupColaboradorModule,
    SmcLookupCicloLetivoModule,
    PoTableModule,
  ],
  exports: [GradeHorarioCompartilhadaFiltroComponent],
})
export class GradeHorarioCompartilhadaModule {}
