import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { EventoAulaDivisaoTurmaModel } from '../../moldels/evento-aula-divisao-turma.model';
import { PoTableColumn } from '@po-ui/ng-components';
import { EventoAulaAgendamentoDetalhesComponent } from '../evento-aula-agendamento-detalhes/evento-aula-agendamento-detalhes.component';
import { Router } from '@angular/router';

@Component({
  selector: 'sga-evento-aula-lista-divisao-turma',
  templateUrl: './evento-aula-lista-divisao-turma.component.html',
})
export class EventoAulaListaDivisaoTurmaComponent implements OnInit {
  colunasTabela: PoTableColumn[] = [
    { property: 'seq', label: 'seq', visible: false },
    { property: 'seqDivisaoTurma', label: 'seq', visible: false },
    { property: 'diaSemanaFormatada', label: 'Data aula', width: '30%' },
    { property: 'horaInicio', label: 'Hora início', width: '5%' },
    { property: 'horaFim', label: 'Hora fim', width: '5%' },
    { property: 'descricaoColaboradores', label: 'Professor(es)', width: '30%' },
    { property: 'local', label: 'Local da aula', width: '30%' },
    {
      property: 'acoes',
      label: ' ',
      type: 'icon',

      icons: [
        {
          value: 'visualizar',
          tooltip: 'Visualizar',
          icon: 'po-icon-eye',
          color: 'color-12',
          action: this.visualizarEvento.bind(this),
        },
      ],
    },
  ];
  @Input('e-divioesTurma') divisoesTurma: EventoAulaDivisaoTurmaModel[] = [];
  @ViewChild('modalAgendamentoDetalhes') modal: EventoAulaAgendamentoDetalhesComponent;

  constructor(private router: Router) {}

  ngOnInit(): void {}

  visualizarEvento(value: any) {
    this.router.navigate([{ outlets: { modais: ['Detalhes', value.seq, value.seqDivisaoTurma] } }], {
      queryParamsHandling: 'preserve',
    });
  }
}
