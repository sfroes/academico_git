import { Component, OnInit, ViewChild } from '@angular/core';
import { PoTableColumn, PoTableComponent } from '@po-ui/ng-components';
import { SituacaoAulaLoteListaModel } from '../models/situacao-aula-lote-lista.model';
import { SituacaoAulaLoteDataService } from '../services/situacao-aula-lote-data.service';
import { SmcModalComponent } from './../../../../../../../shared/components/smc-modal/smc-modal.component';
import { SituacaoAulaLoteService } from './../services/situacao-aula-lote.service';
import { SmcNotificationService } from 'projects/shared/services/notification/smc-notification.service';
import { distinctArray } from 'projects/shared/utils/util';

@Component({
  selector: 'sga-situacao-aula-lote-lista',
  templateUrl: './situacao-aula-lote-lista.component.html',
})
export class SituacaoAulaLoteListaComponent implements OnInit {
  colunas: PoTableColumn[] = [
    { property: 'seq', label: 'Sequencial evento aula', visible: false },
    { property: 'descricaoNivelEnsino', label: 'Descrição nível ensino', visible: false },
    { property: 'prazoAlteracao', label: 'Prazo alteração', visible: false },
    { property: 'codigoDivisaoTurma', label: 'Código de turma formatada', visible: false },
    { property: 'alunosHistoricoEscolar', label: 'Aluno histórico', visible: false },
    { property: 'descricaoDivisaoTurma', label: 'Divisão de turma', width: '40%' },
    { property: 'descricaoColaboradores', label: 'Professor(es)', width: '25%' },
    { property: 'dataHorario', label: 'Data-horário', width: '15%' },
    { property: 'qtdAlunoFalta', label: 'Qts de aluno / Qtd de falta', width: '10%' },
    { property: 'prazoApuracao', label: 'Prazo de apuração', width: '10%' },
  ];
  listaAulas: SituacaoAulaLoteListaModel[] = [];
  desabilitarBtnNaoApurada = true;
  desabilitarBtnNaoExecutada = true;
  desabilitarBtnLiberarApuracao = true;
  desabilitarBtnLiberarCorrecao = true;
  carregando = false;
  mensagemAssert: string;
  mensagemAlert: string;
  alteracaoEscolhida = '';
  exibirMensagemInformativa = false;
  get tokenSegurancaSituacaoAulaLote() {
    return this.dataService.tokensSeguranca.find(f => f.nome === 'situacaoAulaLote')?.permitido;
  }
  @ViewChild(PoTableComponent, { static: true }) tabelaAulas: PoTableComponent;
  @ViewChild('modalConfirmacao') modalConfimacao: SmcModalComponent;
  @ViewChild('modalAlert') modalAlert: SmcModalComponent;

  constructor(
    private dataService: SituacaoAulaLoteDataService,
    private service: SituacaoAulaLoteService,
    private notificationService: SmcNotificationService
  ) {}

  ngOnInit(): void {
    this.dataService.$atualizarListaAulas.subscribe(_ => {
      this.listaAulas = this.dataService.listaAulas;
      this.validarExibicaoBotoes();
    });

    this.dataService.$carregando.subscribe(value => (this.carregando = value));
  }

  alterarAula(acao: string) {
    this.alteracaoEscolhida = acao;
    if (!this.validarAlertSelecaoTurma()) {
      return;
    }

    if (acao === 'NaoApurada' || acao === 'NaoExecutada') {
      if (this.validarAlertAlunoHistoricoEscolar()) {
        return;
      }
      this.assertAcaoNaoExecutadaOuNaoApurada(acao);
    } else if (acao === 'LiberarCorrecao') {
      this.assertAcaoLiberarCorrecao();
    } else {
      this.assertAcaoLiberarApuracao();
    }
  }

  async onSubmitAulas() {
    const seqsEventoAula = this.tabelaAulas.getSelectedRows().map<number>(m => m.seq);
    this.dataService.$carregando.next(true);
    this.modalConfimacao.close();

    if (this.alteracaoEscolhida === 'NaoApurada' || this.alteracaoEscolhida === 'NaoExecutada') {
      await this.service.alterarEventosAulasNaoExecutadaOuNaoApurada(seqsEventoAula, this.alteracaoEscolhida);
      const acaoSelecionada = this.alteracaoEscolhida === 'NaoExecutada' ? 'Não Executada' : 'Não Apurada';
      this.dataService.$atualizarListaAulas.next();
      this.notificationService.success(
        `Situação das aulas alteradas para "${acaoSelecionada}" realizados com sucesso.`
      );
    } else if (this.alteracaoEscolhida === 'LiberarCorrecao') {
      await this.service.liberarEventosAulaCorrecao(seqsEventoAula);
      this.dataService.$atualizarListaAulas.next();
      this.notificationService.success(`Liberação de eventos para correção realizados com sucesso.`);
    } else {
      await this.service.liberarEventosAulaApuracao(seqsEventoAula);
      this.dataService.$atualizarListaAulas.next();
      this.notificationService.success(`Liberação de eventos para apuração realizados com sucesso.`);
    }

    this.dataService.$filtra.next();
  }

  private validarExibicaoBotoes() {
    this.desabilitarBtnNaoApurada = true;
    this.desabilitarBtnNaoExecutada = true;
    this.desabilitarBtnLiberarApuracao = true;
    this.desabilitarBtnLiberarCorrecao = true;
    this.exibirMensagemInformativa = false;

    if (
      !this.tokenSegurancaSituacaoAulaLote &&
      (this.dataService.situacaoEscolhida == 'Não executada' || this.dataService.situacaoEscolhida == 'Executada')
    ) {
      this.exibirMensagemInformativa = true;
      return;
    }

    switch (this.dataService.situacaoEscolhida) {
      case 'Não executada':
        this.desabilitarBtnNaoApurada = false;
        break;
      case 'Não apurada - dentro do prazo':
        this.desabilitarBtnNaoExecutada = false;
        break;
      case 'Não apurada - fora do prazo':
        this.desabilitarBtnLiberarApuracao = false;
        this.desabilitarBtnNaoExecutada = false;
        break;
      default:
        this.desabilitarBtnNaoApurada = false;
        this.desabilitarBtnNaoExecutada = false;
        this.desabilitarBtnLiberarCorrecao = false;
        break;
    }
  }

  private assertAcaoNaoExecutadaOuNaoApurada(acao: string) {
    const aulasSelecionadas = this.tabelaAulas.getSelectedRows();
    const acaoSelecionada = acao === 'NaoExecutada' ? 'Não Executada' : 'Não Apurada';
    let numFalatas = 0;

    aulasSelecionadas.forEach(aula => {
      const faltas = +aula.qtdAlunoFalta.split('/')[1];
      numFalatas += faltas;
    });
    if (numFalatas > 0) {
      this.mensagemAssert = `A situação de ${aulasSelecionadas.length} aulas serão alteradas para "${acaoSelecionada}" e ${numFalatas} 
                             faltas serão apagadas. Deseja continuar?`;
    } else {
      this.mensagemAssert = `A situação de ${aulasSelecionadas.length} aulas serão alteradas para "${acaoSelecionada}". Deseja continhar?`;
    }
    this.modalConfimacao.open();
  }

  private assertAcaoLiberarApuracao() {
    const aulasSelecionadas = this.tabelaAulas.getSelectedRows();

    this.mensagemAssert = `O prazo de alteração ${aulasSelecionadas.length} aula(s) será(ão) alterado(s). Deseja continhar?`;
    this.modalConfimacao.open();
  }

  private assertAcaoLiberarCorrecao() {
    const aulasSelecionadas = this.tabelaAulas.getSelectedRows();
    let niveis = [];
    //Validar se tenho mais de um nivel de ensino com prazo de alteração diferentes
    const todosPrazosAlteracao = distinctArray(aulasSelecionadas.map(m => m.prazoAlteracao));
    if (todosPrazosAlteracao.length > 1) {
      const dadosAulas = aulasSelecionadas.map(m => {
        return { descricaoNivelEnsino: m.descricaoNivelEnsino, prazoAlteracao: m.prazoAlteracao };
      });
      dadosAulas.forEach(aulas => {
        niveis.push(`${aulas.prazoAlteracao}(${aulas.descricaoNivelEnsino})`);
      });
      niveis = distinctArray(niveis);
    }

    if (niveis.length === 0) {
      this.mensagemAssert = `A correção da apuração de ${aulasSelecionadas.length} aulas selecionadas poderá ser realizada num prazo 
                             de ${todosPrazosAlteracao[0]} horas, deseja continuar?`;
    } else {
      this.mensagemAssert = `A correção da apuração de ${aulasSelecionadas.length} aulas selecionadas poderá 
                             ser realizada num prazo de ${niveis.join(', ')} horas, deseja continuar?`;
    }
    this.modalConfimacao.open();
  }

  private validarAlertSelecaoTurma() {
    const aulasSelecionadas = this.tabelaAulas.getSelectedRows();
    let retorno = true;
    if (aulasSelecionadas.length === 0) {
      this.mensagemAlert = 'Selecione ao menos uma aula.';
      this.modalAlert.open();
      retorno = false;
    }
    return retorno;
  }

  private validarAlertAlunoHistoricoEscolar() {
    const aulasSelecionadas = this.tabelaAulas.getSelectedRows().filter(aula => aula.alunosHistoricoEscolar);
    let retorno = false;
    if (aulasSelecionadas.length > 0) {
      const codigoTumas = distinctArray(aulasSelecionadas.map(m => m.codigoDivisaoTurma));
      this.mensagemAlert = `Alteração não permitida, existe aluno com histórico escolar na(s) turma(s): 
                            ${codigoTumas.join(' - ')}`;
      this.modalAlert.open();
      retorno = true;
    }
    return retorno;
  }
}
