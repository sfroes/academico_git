import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SmcLookupCicloLetivoModule } from './../../../../../../shared/components/lookups/lk-cam-002-ciclo-letivo/smc-lookup-ciclo-letivo.module';
import { SmcLookupColaboradorModule } from './../../../../../../shared/components/lookups/lk-dct-001-colaborador/smc-lookup-colaborador.module';
import { SmcLookupTurmaModule } from './../../../../../../shared/components/lookups/lk-tur-001-turma/smc-lookup-turma.module';
import { SharedModule } from './../../../../../../smc-sga-professor/src/app/shared/shared.module';
import { SmcModalModule } from './../../../../../../shared/components/smc-modal/smc-modal.module';
import { SmcButtonModule } from './../../../../../../shared/components/smc-button/smc-button.module';
import { SmcSelectModule } from './../../../../../../shared/components/smc-select/smc-select.module';
import { SituacaoAulaLoteComponent } from './situacao-aula-lote.component';
import { SituacaoAulaLoteFiltroComponent } from './situacao-aula-lote-filtro/situacao-aula-lote-filtro.component';
import { SituacaoAulaLoteListaComponent } from './situacao-aula-lote-lista/situacao-aula-lote-lista.component';
import { PoTableModule } from '@po-ui/ng-components';

@NgModule({
  declarations: [SituacaoAulaLoteComponent, SituacaoAulaLoteFiltroComponent, SituacaoAulaLoteListaComponent],
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
  exports: [SituacaoAulaLoteFiltroComponent],
})
export class SituacaoAulaLoteModule {}
