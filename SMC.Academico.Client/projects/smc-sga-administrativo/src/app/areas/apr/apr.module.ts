import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SituacaoAulaLoteModule } from './situacao-aula-lote/situacao-aula-lote.module';

@NgModule({
  imports: [CommonModule, SituacaoAulaLoteModule],
  exports: [SituacaoAulaLoteModule],
})
export class AprModule {}
