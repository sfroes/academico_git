import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SmcLookupTurmaComponent } from './smc-lookup-turma.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SmcLookupModule } from '../smc-lookup/smc-lookup.module';
import { PoUiModule } from 'projects/shared/modules/po-ui.module';
import { SmcButtonModule } from '../../smc-button/smc-button.module';
import { SmcLookupCicloLetivoModule } from '../lk-cam-002-ciclo-letivo/smc-lookup-ciclo-letivo.module';
import { SmcLookupCursoOfertaModule } from '../lk-cso-001-oferta-curso/smc-lookup-curso-oferta.module';

@NgModule({
  declarations: [SmcLookupTurmaComponent],
  imports: [
    CommonModule,
    SmcLookupModule,
    FormsModule,
    ReactiveFormsModule,
    PoUiModule,
    SmcButtonModule,
    SmcLookupCicloLetivoModule,
    SmcLookupCursoOfertaModule
  ],
  exports: [SmcLookupTurmaComponent],
})
export class SmcLookupTurmaModule {}
