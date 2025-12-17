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
import moment from 'moment';
import { ContextMenuComponent, ContextMenuService } from '@perfectmemory/ngx-contextmenu';
import { SmcLoadService } from 'projects/shared/services/load/smc-load.service';
import { environment } from 'projects/smc-sga-administrativo/src/environments/environment';
import { EventoAulaModel } from '../../moldels/evento-aula.model';
import { EventoAulaDataService } from '../../services/evento-aula-data.service';
import { EventoAulaService } from '../../services/evento-aula.service';
import { EventoAulaAgendamentoDetalhesComponent } from '../evento-aula-agendamento-detalhes/evento-aula-agendamento-detalhes.component';

@Component({
  selector: 'sga-evento-aula-calendario',
  templateUrl: './evento-aula-calendario.component.html',
  standalone: false,
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
  @ViewChild('menubasico') menuContexto!: ContextMenuComponent<Date>;

  colunasTabelaEventos: PoTableColumn[] = [
    { property: 'horaInicio', label: 'Hora inicio' },
    { property: 'horaFim', label: 'Hora fim' },
    { property: '', label: 'Professor(es)' },
    { property: 'local', label: 'Local da aula' },
  ];
  constructor(
    private router: Router,
    private contextMenuService: ContextMenuService<Date>,
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

    if (!this.menuContexto) {
      return;
    }

    // Restaurado com @perfectmemory/ngx-contextmenu v15 (compatível com Angular 15+)
    // A API mudou: agora usa show() como função em vez de Subject
    this.contextMenuService.show(this.menuContexto, {
      x: $event.clientX,
      y: $event.clientY,
      value: date,
    });

    // FIX: A biblioteca está calculando a posição incorretamente
    // Forçar o posicionamento correto manualmente
    setTimeout(() => {
      const menuElement = document.querySelector('context-menu-content') as HTMLElement;
      if (menuElement) {
        menuElement.style.position = 'fixed';
        menuElement.style.top = `${$event.clientY}px`;
        menuElement.style.left = `${$event.clientX}px`;
        menuElement.style.zIndex = '9999';
      }
    }, 0);

    $event.preventDefault();
    $event.stopPropagation();
  }

  async abriModalAgendamentoReduzido(event: any) {
    // API do @perfectmemory/ngx-contextmenu v15 usa event.value em vez de event.item
    const date = event.value || event.item;
    this.router.navigate([{ outlets: { modais: ['AddReduce', moment(date).format('YYYY-MM-DD')] } }], {
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
