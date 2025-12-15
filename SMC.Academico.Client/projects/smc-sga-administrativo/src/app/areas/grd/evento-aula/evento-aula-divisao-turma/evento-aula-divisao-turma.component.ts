import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { SafeHtml } from '@angular/platform-browser';
import { PoModalComponent } from '@po-ui/ng-components';
import { SmcLoadService } from 'projects/shared/services/load/smc-load.service';
import { EventoAulaService } from '../../services/evento-aula.service';
import { EventoAulaDivisaoTurmaModel } from '../../moldels/evento-aula-divisao-turma.model';
import { EventoAulaDataService } from './../../services/evento-aula-data.service';

@Component({
  selector: 'sga-evento-aula-divisao-turma',
  templateUrl: './evento-aula-divisao-turma.component.html',
})
export class EventoAulaDivisaoTurmaComponent implements OnInit {
  checekd = true;
  htmlDetalhe: SafeHtml;
  get tokenSegurancaDetalhes() {
    return this.dataService.tokensSeguranca.find(f => f.nome === 'detalheDivisaoTurmaGrade')?.permitido !== true;
  }
  @Input('e-divisoesTurma') divisoesTurma: EventoAulaDivisaoTurmaModel[] = [];
  @Output('e-checekDivisaoTurma') checekDivisaoTurma = new EventEmitter();

  constructor(
    private smcLoadService: SmcLoadService,
    private eventoAulaService: EventoAulaService,
    private dataService: EventoAulaDataService
  ) {}

  ngOnInit(): void {}

  async abrirModalDetalheDivisaoTurma(seqDivisaoTurma: string, modalDivisaoTurma: PoModalComponent) {
    this.smcLoadService.startLoading();
    try {
      this.htmlDetalhe = await this.eventoAulaService.buscarDetalheDivisaoTurma(seqDivisaoTurma);
      modalDivisaoTurma.open();
    } finally {
      this.smcLoadService.endLoading();
    }
  }

  statusCheckbox(event: any) {
    this.checekDivisaoTurma.emit(event);
  }
}
