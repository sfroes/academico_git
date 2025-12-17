import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CalendarEvent, CalendarView } from 'angular-calendar';
import { SmcColorClassEnum } from 'projects/shared/enum/smc-color-class.enum';
import { SmcLoadService } from 'projects/shared/services/load/smc-load.service';
import { SmcNotificationService } from 'projects/shared/services/notification/smc-notification.service';
import { isNullOrEmpty } from 'projects/shared/utils/util';
import { environment } from 'projects/smc-sga-administrativo/src/environments/environment';
import { EventoAulaDivisaoTurmaModel } from '../../moldels/evento-aula-divisao-turma.model';
import { EventoAulaTurmaCabecalhoModel } from '../../moldels/evento-aula-turma-cabecalho.model';
import { EventoAulaTurmaModel } from '../../moldels/evento-aula-turma.model';
import { EventoAulaModel } from '../../moldels/evento-aula.model';
import { EventoAulaDataService } from '../../services/evento-aula-data.service';
import { EventoAulaService } from '../../services/evento-aula.service';
import { SmcKeyValueModel } from 'projects/shared/models/smc-key-value.model';

@Component({
  selector: 'sga-evento-aula-listar',
  templateUrl: './evento-aula-listar.component.html',
  standalone: false,
})
export class EventoAulaListarComponent implements OnInit {
  view: CalendarView = CalendarView.Month;
  CalendarView = CalendarView;
  viewDate: Date = new Date();
  dadosCabecalhoTurma: EventoAulaTurmaCabecalhoModel;
  divisoesTurma: EventoAulaDivisaoTurmaModel[] = [];
  eventosTurma: EventoAulaModel[] = [];
  divisaoAtualizar: any;
  maximizar = false;
  carregado = false;
  get somenteLeitura() {
    return this.dadosCabecalhoTurma.somenteLeitura;
  }
  @Input('e-seq-turma') seqTurma = '';

  constructor(
    private eventoAulaService: EventoAulaService,
    private smcLoadService: SmcLoadService,
    private dataService: EventoAulaDataService,
    private router: Router,
    private route: ActivatedRoute,
    private notificationService: SmcNotificationService
  ) {}

  ngOnInit(): void {
    this.seqTurma = this.route.snapshot.paramMap.get('seqTurma');
    this.pesquisarTurma(this.seqTurma);
    this.eventoAulaService.refresh.subscribe(_ => {
      this.preencherModelos(this.dataService.dadosTurma);
      this.validacoesTurma();
    });
  }

  habilitarDivisao(divisao: any) {
    const seqDivisao = divisao.target.value;
    this.divisoesTurma.find(f => f.seq === seqDivisao).visivel = divisao.target.checked;
    let eventosTurma: EventoAulaModel[] = [];
    this.divisoesTurma.filter(f => f.visivel).forEach(fd => (eventosTurma = [...eventosTurma, ...fd.eventoAulas]));
    this.eventosTurma = eventosTurma;
  }

  private async pesquisarTurma(seqTurma: string) {
    this.smcLoadService.startLoading();
    try {
      this.dataService.dadosTurma = await this.eventoAulaService.buscarEventosTurma(seqTurma);
      this.eventoAulaService.preencherDataSources();
      this.preencherModelos(this.dataService.dadosTurma);
      this.validacoesTurma();
      this.carregado = true;
    } catch (ex) {
      this.router.navigate(['GRD/EventoAula/Index', seqTurma]);
    } finally {
      this.smcLoadService.endLoading();
    }
  }

  private preencherModelos(model: EventoAulaTurmaModel) {
    this.dadosCabecalhoTurma = model.eventoAulaTurmaCabecalho;
    //Colocando todos os itens como visiveis tanto divisoes quanto eventos
    //Criar links das acoes
    //Seleciona a cor da divisao
    this.divisoesTurma = [];
    const eventosTurma: EventoAulaModel[] = [];
    const coresDivisoes: SmcKeyValueModel[] = [];
    model.eventoAulaDivisoesTurma.forEach((divisao, index) => {
      //Valida se a paleta de cores chegou ao final caso tenha repete
      let selectorCor = index + 1;
      if (selectorCor > 12) {
        selectorCor = 1;
      }
      let corDivisao = SmcColorClassEnum['Color' + selectorCor];
      coresDivisoes.push({ key: divisao.seq, value: corDivisao });
      divisao.visivel = true;
      divisao.cor = corDivisao;
      this.divisoesTurma.push(divisao);
      divisao.eventoAulas.forEach(evento => {
        evento.visivel = true;
        evento.acoes = ['visualizar'];
        evento.cor = corDivisao;
        evento.grupoFormatado = divisao.grupoFormatado;
        eventosTurma.push(evento);
      });
    });
    this.dataService.coresDivisoesTurma = coresDivisoes;
    this.eventosTurma = eventosTurma;
  }

  private validacoesTurma() {
    // Executa apenas na primeira carga da tela
    if (!this.carregado) {
      //UC_GRD_001_01_01.NV15 1 e 2
      // if (!isNullOrEmpty(this.dadosCabecalhoTurma.mensagemFalha)) {
      //   this.notificationService.information(this.dadosCabecalhoTurma.mensagemFalha);
      // }
    }
    //UC_GRD_001_01_01.NV15 3
    const qtdDivisoesCargaCompleta = this.dataService.dadosTurma.eventoAulaDivisoesTurma.filter(
      f => f.cargaHorariaGrade > 0 && f.cargaHorariaLancada >= f.cargaHorariaGrade
    ).length;
    const qtdDivisoes = this.dataService.dadosTurma.eventoAulaDivisoesTurma.length;
    if (qtdDivisoes > 0 && qtdDivisoes === qtdDivisoesCargaCompleta) {
      //this.notificationService.information('Todas as divisões da turma estão com toda a sua carga horária agendada.');
      //Mensagem ficou no topo da tela emcima do combo de turma
      this.dataService.dadosTurma.eventoAulaTurmaCabecalho.cargaHorariaCompleta = true;
      this.dataService.dadosTurma.eventoAulaTurmaCabecalho.mensagemFalha = `Todas as divisões desta turma estão com a carga horária distribuída completa.
         É possível alterar ou excluir agendamentos existentes, para que possam ser incluídos novos.`;
    }
  }
}
