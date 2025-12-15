import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LancamentoNotaComponent } from './lancamento-nota.component';
import { SharedModule } from '../../../shared/shared.module';
import { LancamentoNotaCabecalhoComponent } from './lancamento-nota-cabecalho/lancamento-nota-cabecalho.component';
import { LancamentoNotaObservacaoComponent } from './lancamento-nota-observacao/lancamento-nota-observacao.component';
import { LancamentoNotaFiltroComponent } from './lancamento-nota-filtro/lancamento-nota-filtro.component';
import { LancamentoNotaAlertaComponent } from './lancamento-nota-alerta/lancamento-nota-alerta.component';

@NgModule({
  declarations: [LancamentoNotaComponent, LancamentoNotaCabecalhoComponent,
    LancamentoNotaFiltroComponent, LancamentoNotaObservacaoComponent, LancamentoNotaAlertaComponent],
  imports: [
    CommonModule,
    SharedModule
  ],
  exports: [LancamentoNotaComponent],
})
export class LancamentoNotaModule { }
