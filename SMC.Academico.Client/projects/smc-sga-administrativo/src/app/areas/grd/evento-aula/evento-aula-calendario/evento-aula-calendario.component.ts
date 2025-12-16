import { Component, Input, OnChanges, OnInit, SimpleChanges, ViewChild } from '@angular/core';
import { Meta } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { PoTableColumn } from '@po-ui/ng-components';
import {
  CalendarDayViewBeforeRenderEvent,
  CalendarEvent,
  CalendarMonthViewBeforeRenderEvent,
  CalendarView,
  CalendarWeekViewBeforeRenderEvent,
} from 'angular-calendar';
import * as moment from 'moment';
import { ContextMenuService } from 'ngx-contextmenu';
import { SmcLoadService } from 'projects/shared/services/load/smc-load.service';
import { environment } from 'projects/smc-sga-administrativo/src/environments/environment';
import { EventoAulaModel } from '../../moldels/evento-aula.model';
import { EventoAulaDataService } from '../../services/evento-aula-data.service';
import { EventoAulaService } from '../../services/evento-aula.service';
import { EventoAulaAgendamentoDetalhesComponent } from '../evento-aula-agendamento-detalhes/evento-aula-agendamento-detalhes.component';

@Component({
  selector: 'sga-evento-aula-calendario',
  templateUrl: './evento-aula-calendario.component.html',
})
export class EventoAulaCalendarioComponent implements OnInit, OnChanges {
  visaoPadrao: CalendarView = CalendarView.Month;
  listaEventos: CalendarEvent[] = [];
  get carregandoDataSource$() {
    return this.service.loadingDataSource;
  }
  get tokenSegurancaIncluir() {
    return this.dataService.tokensSeguranca.find(f => f.nome === 'incluirEventoAula')?.permitido !== true;
  }
  @Input('e-eventos-turma') eventosTurma: EventoAulaModel[] = [];
  @ViewChild('modalAgendamentoDetalhes') modal: EventoAulaAgendamentoDetalhesComponent;
  @ViewChild('menuBasico') menuContexto;

  colunasTabelaEventos: PoTableColumn[] = [
    { property: 'horaInicio', label: 'Hora inicio' },
    { property: 'horaFim', label: 'Hora fim' },
    { property: '', label: 'Professor(es)' },
    { property: 'local', label: 'Local da aula' },
  ];
  constructor(
    private router: Router,
    private contextMenuService: ContextMenuService,
    private dataService: EventoAulaDataService,
    private service: EventoAulaService,
    private loadingService: SmcLoadService
  ) {}

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['eventosTurma']) {
      this.preencherCalendario();
    }
  }

  ngOnInit(): void {
    this.preencherCalendario();
    this.service.refresh.subscribe(_ => this.preencherCalendario());
  }

  preencherCalendario() {
    this.listaEventos = this.eventosTurma.map(m => ({
      start: moment(m.data).add(m.horaInicio, 'h').toDate(),
      end: moment(m.data).add(m.horaFim, 'h').toDate(),
      title: m.grupoFormatado,
      meta: m,
    }));
    this.criarEventosFeriado();
    this.criarEventosProcessando();
  }

  visualizarEvento(seqEvento: string, seqDivisaoTurma: string) {
    this.router.navigate([{ outlets: { modais: ['Detalhes', seqEvento, seqDivisaoTurma] } }], {
      queryParamsHandling: 'preserve',
    });
  }

  onContextMenu($event: MouseEvent, date: Date): void {
    date = moment(date).startOf('day').toDate();
    const feriado = this.dataService.dataSourceFeriados?.find(f => date >= f.dataInicio && date <= f.dataFim);
    const naoPulaFeriado = this.dataService.dadosTurma.eventoAulaDivisoesTurma.some(
      s => s.tipoPulaFeriado === 'Não Pula'
    );
    const domingo = date.getDay() === 0;
    const somenteLeitura = this.dataService.dadosTurma.eventoAulaTurmaCabecalho.somenteLeitura;
    const sabado = date.getDay() === 6;
    const aulaSabado = this.dataService.dadosTurma.eventoAulaDivisoesTurma.some(s => s.aulaSabado);
    //O menu de contexto caso alguma divisão esteja complenta
    let algumaDivisaoCargaHorariaCompleta = false;
    this.dataService.dadosTurma.eventoAulaDivisoesTurma.forEach(divisao => {
      const cargaHorariaCompleta = +divisao?.cargaHorariaLancada >= divisao?.cargaHorariaGrade;
      const validacaoAulaSabado = !sabado || divisao.aulaSabado;
      const validacaoFeriado = !feriado || divisao.tipoPulaFeriado === 'Não Pula';
      if (cargaHorariaCompleta && validacaoAulaSabado && validacaoFeriado) {
        algumaDivisaoCargaHorariaCompleta = true;
      }
    });
    if (
      domingo ||
      somenteLeitura ||
      !algumaDivisaoCargaHorariaCompleta ||
      feriado ||
      (!aulaSabado && sabado) ||
      this.tokenSegurancaIncluir
    ) {
      return;
    }
    // TODO: ngx-contextmenu incompatível com Angular 14+ - desabilitado temporariamente
    // Alternativa: adicionar botão de ação no calendário
    /*
    this.contextMenuService.show.next({
      contextMenu: this.menuContexto,
      event: $event,
      item: date,
    });
    $event.preventDefault();
    $event.stopPropagation();
    */
  }

  async abriModalAgendamentoReduzido(event: any) {
    this.router.navigate([{ outlets: { modais: ['AddReduce', moment(event.item).format('YYYY-MM-DD')] } }], {
      queryParamsHandling: 'preserve',
    });
  }

  destacarFeriadosMes(renderEvent: CalendarMonthViewBeforeRenderEvent): void {
    const feriados = this.dataService.dataSourceFeriados;
    const naoPulaFeriado = this.dataService.dadosTurma?.eventoAulaDivisoesTurma.some(
      s => s.tipoPulaFeriado === 'Não Pula'
    );
    if (feriados) {
      renderEvent.body.forEach(day => {
        const feriado = feriados.find(f => day.date >= f.dataInicio && day.date <= f.dataFim);
        const domingo = day.date.getDay() === 0;
        if (feriado || domingo) {
          day.cssClass = 'smc-sga-agendamento-feriado';
          day.meta = { ...day.meta, feriado: true };
        }
        day.meta = { ...day.meta, menuNovoAtivo: !domingo && (naoPulaFeriado || !feriado) };
      });
    }
  }

  destacarFeriadosSemana(renderEvent: CalendarWeekViewBeforeRenderEvent) {
    const feriados = this.dataService.dataSourceFeriados;
    if (!feriados) {
      return;
    }
    renderEvent.hourColumns.forEach(hourColumn => {
      const feriado = feriados.find(f => hourColumn.date >= f.dataInicio && hourColumn.date <= f.dataFim);
      const domingo = hourColumn.date.getDay() === 0;
      hourColumn.hours.forEach(hour => {
        hour.segments.forEach(segment => {
          if (feriado || domingo) {
            segment.cssClass = 'smc-sga-agendamento-feriado';
          }
        });
      });
    });
  }

  destacarFeriadosDia(renderEvent: CalendarDayViewBeforeRenderEvent) {
    const feriados = this.dataService.dataSourceFeriados;
    renderEvent.hourColumns.forEach(hourColumn => {
      const feriado = feriados.find(f => hourColumn.date >= f.dataInicio && hourColumn.date <= f.dataFim);
      const domingo = hourColumn.date.getDay() === 0;
      hourColumn.hours.forEach(hour => {
        hour.segments.forEach(segment => {
          if (feriado || domingo) {
            segment.cssClass = 'smc-sga-agendamento-feriado';
          }
        });
      });
    });
  }

  criarEventosFeriado() {
    if (this.dataService.dataSourceFeriados) {
      this.dataService?.dataSourceFeriados.forEach(feriado => {
        let evento: CalendarEvent = {
          start: feriado.dataInicio,
          end: feriado.dataFim,
          title: feriado.descricaoEvento,
          allDay: true,
          meta: { cor: 'smc-sga-agendamento-desabilitado', visivel: true, feriado: true },
        };
        this.listaEventos.push(evento);
      });
    }
  }

  criarEventosProcessando() {
    this.dataService?.eventoProcessando.forEach(evento => {
      evento.data.setHours(0, 0, 0);
      let newEvento: CalendarEvent = {
        start: moment(evento.data).add(evento.horaInicio, 'h').toDate(),
        end: moment(evento.data).add(evento.horaFim, 'h').toDate(),
        title: evento.grupoFormatado,
        meta: { cor: `smc-sga-agendamento-processando ${evento.cor}`, visivel: true, processando: true },
      };
      this.listaEventos.push(newEvento);
    });
  }
}
