import { Component, OnInit } from '@angular/core';
import { EventoAulaDataService } from '../../services/evento-aula-data.service';
import { Router } from '@angular/router';
import { EventoAulaNotificacaoModel } from '../../moldels/evento-aula-notificacao.model';
import { EventoAulaService } from '../../services/evento-aula.service';
import { isNullOrEmpty } from 'projects/shared/utils/util';
import { SmcKeyValueModel } from 'projects/shared/models/smc-key-value.model';
import * as moment from 'moment';

@Component({
  selector: 'sga-evento-aula-notificacoes',
  templateUrl: './evento-aula-notificacoes.component.html',
})
export class EventoAulaNotificacoesComponent implements OnInit {
  exibir = false;
  listaNotificacoes: EventoAulaNotificacaoModel[] = [];

  constructor(private dataService: EventoAulaDataService, private service: EventoAulaService) {}

  ngOnInit(): void {
    this.preencherNotificacoes();
    this.service.refresh.subscribe(_ => this.preencherNotificacoes());
  }

  exibirNotificacao() {
    this.exibir = !this.exibir;
  }

  preencherNotificacoes() {
    //TODO: Simplificar parse da data
    const notificacoes: EventoAulaNotificacaoModel[] = [];
    const notificacoesLocal: EventoAulaNotificacaoModel[] = [];
    const notificacoesColaborador: EventoAulaNotificacaoModel[] = [];
    this.dataService.dadosTurma.eventoAulaDivisoesTurma.forEach(divisao => {
      divisao.eventoAulas.forEach(evento => {
        if (isNullOrEmpty(evento.local)) {
          notificacoesLocal.push({
            seqEvento: evento.seq,
            seqDivisaoTurma: evento.seqDivisaoTurma,
            seqHorarioAgd: evento.seqHorarioAgd,
            codigoRecorrencia: evento.codigoRecorrencia,
            descricao: `O{s} {x} agendamento{s} de ${evento.diaSemanaDescricao} de ${evento.horaInicio} à ${evento.horaFim}, 
            referente{s} a divisão de turma ${divisao.grupoFormatado}, {es} sem local vinculado.`,
            dataOrdenacao: moment(moment(evento.data).format('YYYY-MM-DD') + evento.horaInicio, 'YYYY-MM-DDhh:mm')
              .toDate()
              .getTime(),
          });
        }
        if (evento.colaboradores.filter(f => !isNullOrEmpty(f.seqColaborador)).length === 0) {
          notificacoesColaborador.push({
            seqEvento: evento.seq,
            seqDivisaoTurma: evento.seqDivisaoTurma,
            seqHorarioAgd: evento.seqHorarioAgd,
            codigoRecorrencia: evento.codigoRecorrencia,
            descricao: `O{s} {x} agendamento{s} de ${evento.diaSemanaDescricao} de ${evento.horaInicio} à ${evento.horaFim}, 
                        referente{s} à divisão de turma ${divisao.grupoFormatado}, {es} sem pesquisador(es)/professor(es) vinculado(s) por não alocação 
                        ou por não possuir vinculo ativo durante o período de agendamento.`,
            dataOrdenacao: moment(moment(evento.data).format('YYYY-MM-DD') + evento.horaInicio, 'YYYY-MM-DDhh:mm')
              .toDate()
              .getTime(),
          });
        }
      });
    });
    notificacoes.push(...this.agruparEventosPorRecorrencia(notificacoesLocal));
    notificacoes.push(...this.agruparEventosPorRecorrencia(notificacoesColaborador));
    this.listaNotificacoes = notificacoes.sort((a, b) => +a.seqHorarioAgd - +b.seqHorarioAgd);
  }

  agruparEventosPorRecorrencia(eventos: EventoAulaNotificacaoModel[]): EventoAulaNotificacaoModel[] {
    const groups = new Map<string, EventoAulaNotificacaoModel[]>();
    const result: EventoAulaNotificacaoModel[] = [];

    eventos.forEach(evento => {
      if (!groups.has(`${evento.seqHorarioAgd}${evento.seqDivisaoTurma}`)) {
        groups.set(`${evento.seqHorarioAgd}${evento.seqDivisaoTurma}`, []);
      }
      groups.get(`${evento.seqHorarioAgd}${evento.seqDivisaoTurma}`).push(evento);
    });

    for (const key of groups.keys()) {
      const ordenado = groups.get(key).sort((a, b) => a.dataOrdenacao - b.dataOrdenacao);
      ordenado[0].descricao = this.replaceMensagens(ordenado.length, ordenado[0].descricao);
      result.push(ordenado[0]);
    }

    return result;
  }

  replaceMensagens(numeroEventos: number, mensagem: string) {
    let mensagemReplace = '';
    if (numeroEventos > 1) {
      mensagemReplace = mensagem.replace('{x}', numeroEventos.toString());
      mensagemReplace = mensagemReplace.split('{s}').join('s');
      mensagemReplace = mensagemReplace.replace('{es}', 'estão');
    } else {
      mensagemReplace = mensagem.replace('{x}', '');
      mensagemReplace = mensagemReplace.split('{s}').join('');
      mensagemReplace = mensagemReplace.replace('{es}', 'está');
    }

    return mensagemReplace;
  }
}
