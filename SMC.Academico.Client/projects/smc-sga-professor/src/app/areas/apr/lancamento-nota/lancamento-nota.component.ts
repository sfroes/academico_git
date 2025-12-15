import { DOCUMENT } from '@angular/common';
import { Component, Inject, OnInit, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { AbstractControl, FormArray, FormGroup } from '@angular/forms';
import { DomSanitizer, Title } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { PoInputComponent } from '@po-ui/ng-components';
import { SmcLoadService } from 'projects/shared/services/load/smc-load.service';
import { SmcNotificationService } from 'projects/shared/services/notification/smc-notification.service';
import { configurarNavegacoesLinha, distinctArray, isNullOrEmpty } from 'projects/shared/utils/util';
import { interval } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import { AppConstants } from '../../../app.constants';
import { LancamentoNotaService } from '../services/lancamento-nota.service';
import { LancamentoNotaAlertaComponent } from './lancamento-nota-alerta/lancamento-nota-alerta.component';
import { LancamentoNotaFiltroComponent } from './lancamento-nota-filtro/lancamento-nota-filtro.component';
import { LancamentoNotaObservacaoComponent } from './lancamento-nota-observacao/lancamento-nota-observacao.component';
import { LancamentoNotaAlunoApuracaoModel } from './models/lancamento-nota-aluno-apuracao.model';
import { LancamentoNotaAlunoModel } from './models/lancamento-nota-aluno.model';
import { LancamentoNotaAvaliacaoModel } from './models/lancamento-nota-avaliacao.model';
import { LancamentoNotaModel } from './models/lancamento-nota.model';
import { LancamentoNotaMapService } from './services/lancamento-nota-map.service';
import { SmcAssertComponent } from 'projects/shared/components/smc-assert/smc-assert.component';

@Component({
  selector: 'app-lancamento-nota',
  templateUrl: './lancamento-nota.component.html',
  styleUrls: ['./lancamento-nota.component.scss'],
})
export class LancamentoNotaComponent implements OnInit {
  get descricaoComponenteSafeHtml() {
    return this.sanitizer.bypassSecurityTrustHtml('Origem avaliação: ' + this.model.descricaoOrigemAvaliacao);
  }
  model: LancamentoNotaModel;
  formNotas: FormGroup;
  maximizar = false;
  registrosAlterados = 0;
  nomesAlunoAlerta: string[] = [];
  mensagemAlerta = '';
  procesandoMateraLecionada = false;
  tokenSegurancaComentarioApuracao = true;
  @ViewChildren('nota') elementosNota: QueryList<PoInputComponent>;
  @ViewChild(LancamentoNotaFiltroComponent)
  filtro: LancamentoNotaFiltroComponent;

  constructor(
    private title: Title,
    private activatedRoute: ActivatedRoute,
    @Inject(DOCUMENT) private document: Document,
    private lancamentoNotaService: LancamentoNotaService,
    private lancamentoNotaMapService: LancamentoNotaMapService,
    private loadingService: SmcLoadService,
    private notificationService: SmcNotificationService,
    private sanitizer: DomSanitizer
  ) {}

  ngOnInit() {
    this.title.setTitle(AppConstants.TITLE + 'Lançamento Notas');

    // Utiliza o resolver service para ter os dados antes de montar a tela
    this.activatedRoute.data.subscribe(data => {
      this.preencherModelo(data.model);
      this.lancamentoNotaService.buscarTokensSeguranca().then(result => {
        this.tokenSegurancaComentarioApuracao = result.find(f => f.nome === 'comentarioApuracao')?.permitido !== true;
      });
      this.loadingService.endLoading();
    });

    configurarNavegacoesLinha(this.elementosNota);
    this.elementosNota.changes.subscribe(m => configurarNavegacoesLinha(m));
  }

  valorMateriaLecionada(valor: any) {
    this.formNotas.controls.materiaLecionada.setValue(valor);
    this.salvarMateriaLecionada();
  }

  private preencherHistorico(reg: LancamentoNotaModel) {
    reg.alunos.forEach(f => {
      if (f.temHistorico) {
        f.totalHistorico = f.total;
        f.situacaoHistorico = f.situacaoFinal;
        f.descricaoSituacaoHistorico = f.descricaoSituacaoFinal;
      }
    });
  }

  private preencherModelo(reg: LancamentoNotaModel) {
    this.preencherHistorico(reg);
    this.model = reg;
    this.model.avaliacoes.forEach(f => {
      f.mostrar = true;
      f.descricaoFormatada = `${f.sigla} - ${f.descricao}: ${f.valor} ${f.valor === 1 ? 'ponto' : 'pontos'}`;
    });
    this.model.alunos.forEach(f => (f.mostrar = true));

    this.formNotas = this.lancamentoNotaMapService.mapearForm(this.model);
    if (!this.model.diarioFechado) {
      this.configurarDependency();
    }
    if (this.model.alunos.length > 0) {
      if (this.model.origemAvaliacaoTurma) {
        this.calcularTotaisTurma();
      } else {
        this.calcularTotaisDivisaoTurma();
      }
    }
    this.filtro?.aplicarFiltro();
  }

  private configurarDependency() {
    this.model.alunos.forEach((aluno, indexAluno) => {
      const groupAluno = this.formNotas.get(`alunos.${indexAluno}`) as FormGroup;
      // Configura o "depency para calcular o total"
      groupAluno.valueChanges.pipe(debounceTime(500)).subscribe(() => {
        if (groupAluno.valid) {
          if (this.model.origemAvaliacaoTurma) {
            if (aluno.totalDivisaoTurmaCalculado) {
              this.calcularTotalTurmaAluno(aluno, groupAluno);
            }
          } else {
            this.calcularTotalDivisaoTurmaAluno(aluno, groupAluno);
          }
        }
        this.atualizarTotalRegistrosAlterados();
      });
    });
  }

  private atualizarTotalRegistrosAlterados() {
    const alunos = this.formNotas.get('alunos') as FormArray;
    this.registrosAlterados = alunos.controls.filter(f => f.dirty).length;
  }

  private calcularTotaisTurma() {
    this.lancamentoNotaService
      .calcularTotaisTurma(
        this.model.seqOrigemAvaliacao,
        this.model.permiteAlunoSemNota,
        this.model.alunos.map(m => m.seqAlunoHistorico)
      )
      .subscribe(dadosAlunos => {
        dadosAlunos.forEach((alunoData, indexAluno) => {
          const alunoModel = this.model.alunos.filter(f => f.seqAlunoHistorico === alunoData.seqAlunoHistorico)[0];
          alunoModel.totalParcial = alunoData.totalParcial;
          alunoModel.totalDivisaoTurmaCalculado = true;
          const alunoForm = this.formNotas.get(`alunos.${indexAluno}`) as FormGroup;
          if (alunoForm.dirty) {
            this.calcularTotalTurmaAluno(alunoModel, alunoForm);
            return;
          }
          alunoModel.total = alunoData.totalFinal;
          alunoModel.totalCalculado = true;
          alunoModel.situacaoFinal = alunoData.situacaoFinal;
          alunoModel.descricaoSituacaoFinal = alunoData.descricaoSituacaoFinal;
          alunoModel.situacaoFinalCalculada = true;
          alunoModel.todasApuracoesDivisaoLancadas = alunoData.todasApuracoesDivisaoLancadas;
          alunoModel.todasApuracoesVazias = alunoData.todasApuracoesVazias;
        });
      });
  }

  private calcularTotalTurmaAluno(aluno: LancamentoNotaAlunoModel, alunoGroup: FormGroup) {
    if (!this.model.permiteAlunoSemNota && isNullOrEmpty(aluno.totalParcial)) {
      return;
    }
    const arrayApuracoes = alunoGroup.get('apuracoes');
    aluno.totalCalculado = false;
    let totalParcial: number = null;
    if (aluno.totalParcial === '*') {
      totalParcial = 0;
    } else if (!isNullOrEmpty(aluno.totalParcial)) {
      totalParcial = +aluno.totalParcial;
    }
    this.lancamentoNotaService
      .calcularTotalTurma(this.model.seqOrigemAvaliacao, aluno.seqAlunoHistorico, totalParcial, arrayApuracoes)
      .toPromise()
      .then(notaTotal => {
        aluno.total = notaTotal;
        this.calcularSituacaoFinal(aluno, alunoGroup);
        aluno.totalCalculado = true;
      });
  }

  private calcularSituacaoFinal(aluno: LancamentoNotaAlunoModel, alunoForm: AbstractControl) {
    aluno.situacaoFinalCalculada = false;
    if (!this.model.permiteAlunoSemNota && (aluno.total === null || !aluno.todasApuracoesDivisaoLancadas)) {
      aluno.situacaoFinal = null;
      aluno.situacaoFinalCalculada = true;
      return;
    }
    this.lancamentoNotaService
      .calcularSituacaoFinal(this.model.seqOrigemAvaliacao, aluno.seqAlunoHistorico, aluno.total)
      .toPromise()
      .then(situacao => {
        aluno.situacaoFinal = situacao.valor;
        aluno.descricaoSituacaoFinal = situacao.descricao;
        aluno.situacaoFinalCalculada = true;
      });
  }

  private calcularTotaisDivisaoTurma() {
    this.lancamentoNotaService
      .calcularTotaisDivisaoTurma(
        this.model.seqOrigemAvaliacao,
        this.model.alunos.map(m => m.seqAlunoHistorico)
      )
      .toPromise()
      .then(dadosAlunos => {
        dadosAlunos.forEach((dadoAluno, indexAluno) => {
          const alunoModel = this.model.alunos[indexAluno];
          const alunoForm = this.formNotas.get(`alunos.${indexAluno}`) as FormGroup;
          if (alunoForm.dirty) {
            this.calcularTotalDivisaoTurmaAluno(alunoModel, alunoForm);
            return;
          }
          alunoModel.total = dadoAluno.total;
          alunoModel.totalCalculado = true;
        });
      });
  }

  private calcularTotalDivisaoTurmaAluno(aluno: LancamentoNotaAlunoModel, alunoGroup: FormGroup) {
    aluno.totalCalculado = false;
    const arrayApuracoes = alunoGroup.get('apuracoes');
    this.lancamentoNotaService
      .calcularTotalDivisaoTurma(this.model.seqOrigemAvaliacao, aluno.seqAlunoHistorico, arrayApuracoes)
      .toPromise()
      .then(notaTotal => {
        aluno.total = notaTotal;
        aluno.totalCalculado = true;
      });
  }

  filtarAvaliacoes(seqs: string[]) {
    this.model.avaliacoes.forEach(f => (f.mostrar = seqs.some(s => s === f.seqAplicacaoAvaliacao)));
  }

  filtrarAlunos(filtro: { ra: string; nome: string }) {
    this.model.alunos.forEach(
      f =>
        (f.mostrar =
          f.numeroRegistroAcademico.indexOf(filtro.ra) > -1 &&
          f.nome.toUpperCase().indexOf(filtro.nome.toUpperCase()) > -1)
    );
  }

  abrirAvaliacaoWeb(avaliacao: LancamentoNotaAvaliacaoModel, assertNavegacao: SmcAssertComponent) {
    // tslint:disable-next-line: max-line-length
    const url = `../APR/EntregaOnline?seqAplicacaoAvaliacao=${avaliacao.seqAplicacaoAvaliacao}&seqOrigemAvaliacao=${this.model.seqOrigemAvaliacao}&backLancamentoNota=true`;
    if (this.formNotas.dirty) {
      assertNavegacao.confirmCallback = () => (this.document.location.href = url);
      assertNavegacao.showMessage(
        'Existem dados não gravados, deseja prosseguir?',
        'Confirmar saída do lançamento de notas'
      );
    } else {
      this.document.location.href = url;
    }
  }

  exibirLegendaHistoricoAluno(aluno: LancamentoNotaAlunoModel) {
    if (aluno.temHistorico) {
      if (aluno.situacaoFinal !== aluno.situacaoHistorico || aluno.total !== aluno.totalHistorico) {
        return 'smc-btn-historico-divergente';
      } else {
        return 'smc-btn-historico-preenchido';
      }
    } else {
      return 'smc-btn-historico';
    }
  }

  exibirLegendaObservacaoAluno(indexAluno: number) {
    if (
      this.formNotas.value.alunos[indexAluno].apuracoes.filter(f => !isNullOrEmpty(f.comentarioApuracao)).length === 0
    ) {
      return 'smc-btn-comentario';
    } else {
      return 'smc-btn-comentario-preenchido';
    }
  }

  exibirApuracaoDesabilitada(indexApuracao: number, aluno: LancamentoNotaAlunoModel) {
    return (
      this.model.diarioFechado ||
      (aluno.apuracoes[indexApuracao].entregaWeb &&
        !aluno.apuracoes[indexApuracao].permitirAlunoEntregarOnlinePosPrazo) ||
      aluno.apuracoes[indexApuracao].alunoComComponenteOutroHistorico ||
      aluno.formado ||
      aluno.alunoDispensado ||
      aluno.alunoAprovado ||
      aluno.processandoAtualizacaoHistorico
    );
  }

  exibirTag(aluno: LancamentoNotaAlunoModel): boolean {
    let retorno = false;
    retorno = aluno.alunoAprovado || aluno.alunoDispensado || aluno.formado || aluno.temHistorico;
    return retorno;
  }

  exibirDescriacoTag(aluno: LancamentoNotaAlunoModel): string {
    let retorno: string;
    if (aluno.formado) {
      retorno = 'Formado';
    } else if (aluno.alunoDispensado) {
      retorno = 'Dispensado';
    } else if (aluno.alunoAprovado) {
      retorno = 'Concluído';
    } else if (aluno.temHistorico) {
      retorno = 'Histórico Escolar';
    }

    return retorno;
  }

  exibirTotalHistorico(aluno: LancamentoNotaAlunoModel) {
    if (this.model.origemAvaliacaoTurma && aluno.totalHistorico !== undefined && aluno.total !== aluno.totalHistorico) {
      return `Valor do histórico: ${aluno.totalHistorico}`;
    }
    return '';
  }

  exibirSituacaoHistorico(aluno: LancamentoNotaAlunoModel) {
    if (
      this.model.origemAvaliacaoTurma &&
      aluno.temHistorico &&
      aluno.descricaoSituacaoFinal !== undefined &&
      aluno.descricaoSituacaoFinal !== aluno.descricaoSituacaoHistorico
    ) {
      return `Valor do histórico: ${aluno.descricaoSituacaoHistorico}`;
    }
    return '';
  }

  abrirModalObservacao(indexAluno: number, modal: LancamentoNotaObservacaoComponent) {
    const apuracoesModel: LancamentoNotaAlunoApuracaoModel[] = [];
    const alunoModel = this.model.alunos[indexAluno];

    for (let n = 0; n < alunoModel.apuracoes.length; n++) {
      const apuracaoModel = this.formNotas.value.alunos[indexAluno].apuracoes[n] as LancamentoNotaAlunoApuracaoModel;
      apuracaoModel.comparecimento = apuracaoModel.nota !== '*';
      apuracaoModel.entregaWeb = alunoModel.apuracoes[n].entregaWeb;
      apuracoesModel.push(apuracaoModel);
    }

    modal.abrirModal(
      alunoModel.seqAlunoHistorico,
      alunoModel.formado ? `${alunoModel.nome} - FORMADO` : alunoModel.nome,
      alunoModel.formado,
      apuracoesModel
    );
  }

  atualizarObservacao(apuracoes: LancamentoNotaAlunoApuracaoModel[]) {
    const indexAluno = this.model.alunos.findIndex(a => a.seqAlunoHistorico === apuracoes[0].seqAlunoHistorico);
    apuracoes.forEach(apuracao => {
      const indiceApuracao = this.model.alunos[indexAluno].apuracoes.findIndex(
        a => a.seqAplicacaoAvaliacao === apuracao.seqAplicacaoAvaliacao
      );

      const nota = this.formNotas.get(`alunos.${indexAluno}.apuracoes.${indiceApuracao}.nota`);
      nota.setValue(apuracao.nota);
      nota.markAsDirty();

      const comentarioApuracao = this.formNotas.get(
        `alunos.${indexAluno}.apuracoes.${indiceApuracao}.comentarioApuracao`
      );
      comentarioApuracao.setValue(apuracao.comentarioApuracao);
      comentarioApuracao.markAsDirty();
    });
  }

  aguardarProcessamento(): Promise<any> {
    return new Promise(resolve => {
      const processamentoTotalSubscription = interval(100).subscribe(() => {
        const calculado = this.model.alunos.some(
          s => s.totalDivisaoTurmaCalculado && s.totalCalculado && s.situacaoFinalCalculada
        );

        if (calculado) {
          processamentoTotalSubscription.unsubscribe();
          resolve(null);
        }
      });
    });
  }

  abrirModalHistorico(modal: LancamentoNotaAlertaComponent) {
    // tslint:disable-next-line: max-line-length
    const mensagem = `Alguma nota ou frequência do aluno sofreu alteração e a situação final do aluno está diferente do histórico escolar. A atualização pode ser realizada para o aluno individualmente ou será automática para todos os alunos quando o diário for fechado`;
    modal.abrirModalUnica(mensagem);
    return;
  }

  async validarAssertAtualizacaoHistorico(
    indexAluno: number,
    modal: LancamentoNotaAlertaComponent,
    asserAtualizarHistorico: SmcAssertComponent
  ) {
    const aluno = this.model.alunos[indexAluno];

    aluno.processandoAtualizacaoHistorico = true;
    await this.aguardarProcessamento();
    aluno.processandoAtualizacaoHistorico = false;

    //se aluno sem nenhuma nota
    //se turma permite salvar aluno sem nota, salvar a "situação aluno sem nota"
    //se turma não permite, perguntar: "O aluno xxx não possui notas lançadas, deseja excluir o histórico escolar deste componente?"
    if (!this.model.permiteAlunoSemNota && aluno.temHistorico) {
      if (isNullOrEmpty(aluno.totalParcial)) {
        aluno.processandoAtualizacaoHistorico = true;
        asserAtualizarHistorico.confirmCallback = () =>
          this.ExcluirHitoricoEscolar(aluno.seqAlunoHistorico, this.model.seqOrigemAvaliacao, aluno.nome);
        asserAtualizarHistorico.showMessage(
          `O(a) aluno(a) ${aluno.nome} não possui notas lançadas, deseja excluir o histórico escolar deste componente?.`,
          'Confirmar atualização de histórico'
        );
        aluno.processandoAtualizacaoHistorico = false;
        return;
      }
    }

    //se aluno tem pelo menos 1 nota
    //erro só permite salvar se tiver todas as notas
    //mesmo se a turma permitr salvar "sem nota" mas tiver lancado "uma" nota é necessário que lance todas
    if (
      (!this.model.permiteAlunoSemNota && !aluno.todasApuracoesDivisaoLancadas) ||
      (this.model.permiteAlunoSemNota && !aluno.todasApuracoesVazias && !aluno.todasApuracoesDivisaoLancadas) ||
      (this.model.permiteAlunoSemNota && !isNullOrEmpty(aluno.totalParcial) && !aluno.todasApuracoesDivisaoLancadas)
    ) {
      // tslint:disable-next-line: max-line-length
      const mensagem = `O(a) aluno(a) ${aluno.nome} possui avaliação sem nota lançada, é necessário que todas as notas exceto reavaliação estejam lançada para atualizar o histórico escolar.`;
      modal.abrirModalUnica(mensagem);
      return;
    }

    asserAtualizarHistorico.confirmCallback = () => this.fechamentoDiarioIndividual(aluno, indexAluno);
    asserAtualizarHistorico.showMessage(
      `Confirma a atualização de histórico do aluno ${aluno.nome}?`,
      'Confirmar atualização de histórico'
    );
  }

  async ExcluirHitoricoEscolar(seqAlunoHistorico: string, seqOrigemAvaliacao: string, nome: string) {
    this.notificationService.information(`Iniciando a atualização de histórico do(a) aluno(a): ${nome}.`);
    this.lancamentoNotaService
      .excluirHistoricoEscolar(seqAlunoHistorico, seqOrigemAvaliacao)
      .toPromise()
      .then(data => {
        this.notificationService.success(`Histórico atualizado do(a) aluno(a) ${nome} com sucesso!`);
        this.preencherModelo(data);
      });
  }

  async fechamentoDiarioIndividual(alunoModel: LancamentoNotaAlunoModel, indexAluno: number) {
    alunoModel.processandoAtualizacaoHistorico = true;
    await this.salvarIndividual(indexAluno, alunoModel.seqAlunoHistorico);
    const modelo = this.lancamentoNotaMapService.mapearRetornoAtualizacaoHistorico(
      this.model,
      this.formNotas,
      indexAluno
    );
    this.notificationService.information(`Iniciando a atualização de histórico do(a) aluno(a): ${alunoModel.nome}.`);
    this.lancamentoNotaService
      .fecharDiario(modelo)
      .toPromise()
      .then(() => {
        alunoModel.temHistorico = true;
        alunoModel.totalHistorico = alunoModel.total;
        alunoModel.situacaoHistorico = alunoModel.situacaoFinal;
        alunoModel.descricaoSituacaoHistorico = alunoModel.descricaoSituacaoFinal;
        this.notificationService.success(`Histórico atualizado do(a) aluno(a) ${alunoModel.nome} com sucesso!`);
      })
      .finally(() => {
        alunoModel.processandoAtualizacaoHistorico = false;
      });
  }

  private salvarIndividual(indexAluno: number, seqAlunoHistorico: string): Promise<any> {
    const alunoForm = this.formNotas.get(`alunos.${indexAluno}`);
    if (alunoForm.pristine) {
      return;
    }
    const model = this.lancamentoNotaMapService.mapearRetornoModelo(this.model, this.formNotas, seqAlunoHistorico);
    return this.lancamentoNotaService
      .salvarLancamento(model)
      .toPromise()
      .then(() => alunoForm.markAsPristine());
  }

  validarAssertSalvar(assert: SmcAssertComponent, modal: LancamentoNotaAlertaComponent): Promise<any> {
    return new Promise(resolve => {
      const alunosFormDirty = (this.formNotas.get('alunos') as FormArray).controls.filter(f => f.dirty);
      const seqsAlunosDirty = alunosFormDirty.map(m => m.value.seqAlunoHistorico as string);
      const alunosModelDirty = this.model.alunos.filter(f => seqsAlunosDirty.includes(f.seqAlunoHistorico));

      if (this.validarAlunosAlteradosNaoExibidos(alunosFormDirty)) {
        assert.confirmCallback = () => this.salvar().then(() => resolve(null));
        assert.showMessage(
          'Existem dados alterados, que não estão sendo exibidos na tela, e serão gravados.',
          'Confirmar salvar notas'
        );
      } else {
        const nomesAlunoComObservacaoSemNota = this.validarAlunosComObservacaoSemNota(
          alunosFormDirty,
          alunosModelDirty
        );
        if (nomesAlunoComObservacaoSemNota.length > 0) {
          const mensagem = 'Estão com comentário e sem nota, favor informar uma nota ou remover o comentário.';
          modal.abrirModal(mensagem, nomesAlunoComObservacaoSemNota);
          return;
        } else {
          this.salvar().then(() => resolve(null));
        }
      }
    });
  }

  salvar() {
    this.loadingService.startLoading();
    this.notificationService.information('Iniciando gravação.');
    const modelo = this.lancamentoNotaMapService.mapearRetornoModelo(this.model, this.formNotas);
    return this.lancamentoNotaService
      .salvarLancamento(modelo)
      .toPromise()
      .then(data => {
        this.notificationService.success('Lançamento gravado com sucesso!');
        this.preencherModelo(data);
      })
      .finally(() => this.loadingService.endLoading());
  }

  async salvarMateriaLecionada() {
    const modelo = this.lancamentoNotaMapService.mapearRetornoMateriaLecionada(this.formNotas);
    this.procesandoMateraLecionada = true;
    this.notificationService.information('Iniciando gravação da matéria lecionada.');
    this.lancamentoNotaService
      .salvarLancamento(modelo)
      .toPromise()
      .then(data => {
        const materiaLecionada = this.formNotas.value.materiaLecionada.trim();
        this.formNotas.controls.materiaLecionada.setValue(materiaLecionada);
        this.notificationService.success('Matéria lecionada gravada com sucesso!');
        this.preencherModelo(data);
      })
      .finally(() => (this.procesandoMateraLecionada = false));
  }

  private validarAlunosComObservacaoSemNota(
    alunosFormDirty: AbstractControl[],
    alunosModelDirty: LancamentoNotaAlunoModel[]
  ) {
    const result: string[] = [];
    alunosFormDirty.forEach(alunoForm => {
      alunoForm.value.apuracoes.forEach(apuracao => {
        if (!isNullOrEmpty(apuracao.comentarioApuracao) && isNullOrEmpty(apuracao.nota)) {
          const nome = alunosModelDirty.filter(
            alunoModel => alunoModel.seqAlunoHistorico === alunoForm.value.seqAlunoHistorico
          )[0].nome;
          result.push(nome);
        }
      });
    });
    return distinctArray(result);
  }

  private validarAvaliacaoExibida(seqAplicacaoAvaliacao: string) {
    return this.model.avaliacoes.filter(f => f.seqAplicacaoAvaliacao === seqAplicacaoAvaliacao)[0].mostrar;
  }

  private validarAlunoExibido(seqAlunoHistorico: string) {
    return this.model.alunos.filter(f => f.seqAlunoHistorico === seqAlunoHistorico)[0].mostrar;
  }

  private validarAlunosAlteradosNaoExibidos(alunosFormDirty: AbstractControl[]) {
    for (const alunoForm of alunosFormDirty) {
      for (const apuracaoAluno of (alunoForm.get('apuracoes') as FormArray).controls) {
        if (
          apuracaoAluno.dirty &&
          !(
            this.validarAvaliacaoExibida(apuracaoAluno.value.seqAplicacaoAvaliacao) &&
            this.validarAlunoExibido(alunoForm.value.seqAlunoHistorico)
          )
        ) {
          return true;
        }
      }
    }
    return false;
  }

  async validarAssertFecharDiario(assert: SmcAssertComponent, modal: LancamentoNotaAlertaComponent) {
    this.loadingService.startLoading();
    await this.aguardarProcessamento();
    this.loadingService.endLoading();
    this.nomesAlunoAlerta = this.recuperarNomeAlunoSemSituacao();
    const titulo = 'Confirmar fechamento diário';
    this.mensagemAlerta = '';
    assert.confirmCallback = () => this.fecharDiario(assert, modal);

    if (this.model.materiaLecionadaCadastrada) {
      const mensagem = 'Operação não permitida, as seguintes divisões de turma estão sem matéria lecionada cadastrada:';
      modal.abrirModalUnicaComLista(mensagem, this.model.descricoesDivisaoTurma);
    } else if (this.model.permiteAlunoSemNota && this.nomesAlunoAlerta.length > 0) {
      // tslint:disable-next-line: max-line-length
      this.mensagemAlerta =
        "O(s) aluno(s) listado(s) abaixo não possuem nota lançada, a situação final será gravada como 'Aluno sem nota/conceito'.";
      assert.showMessage(null, titulo);
    } else if (this.model.permiteAlunoSemNota && this.nomesAlunoAlerta.length === 0) {
      const mensagem = 'Não será possível alterar as notas após o fechamento do diário.';
      assert.showMessage(mensagem, titulo);
    } else {
      if (this.nomesAlunoAlerta.length > 0) {
        // tslint:disable-next-line: max-line-length
        const mensagem =
          'O(s) aluno(s) listado(s) abaixo não possuem nota lançada, é necessário que todos os alunos possuam notas para realizar o fechamento do diário';
        modal.abrirModal(mensagem, this.nomesAlunoAlerta);
      } else {
        const mensagem = 'Não será possível alterar as notas após o fechamento do diário.';
        assert.showMessage(mensagem, titulo);
      }
    }
  }

  private recuperarNomeAlunoSemSituacao() {
    const nomesAlunosSemSituacao: string[] = [];
    const indexAlunosSemSituacao = this.recuperarIndexAlunoSemSituacao();
    this.model.alunos.forEach((aluno, index) => {
      if (indexAlunosSemSituacao.includes(index)) {
        nomesAlunosSemSituacao.push(aluno.nome);
      }
    });
    return nomesAlunosSemSituacao;
  }

  private recuperarIndexAlunoSemSituacao() {
    const indexAlunosSemSituacao: number[] = [];
    this.model.alunos.forEach((aluno, index) => {
      if (isNullOrEmpty(aluno.descricaoSituacaoFinal)) {
        indexAlunosSemSituacao.push(index);
      }
    });
    return distinctArray(indexAlunosSemSituacao);
  }

  async fecharDiario(assert: SmcAssertComponent, modal: LancamentoNotaAlertaComponent) {
    if (this.formNotas.dirty) {
      await this.validarAssertSalvar(assert, modal);
      this.loadingService.startLoading();
      await this.aguardarProcessamento();
    } else {
      this.loadingService.startLoading();
    }
    this.notificationService.information('Iniciando fechamendo do diário.');
    const modelDiario = this.lancamentoNotaMapService.mapearRetornoFechamentoDiario(this.model, this.formNotas);
    this.lancamentoNotaService
      .fecharDiario(modelDiario)
      .toPromise()
      .then(data => {
        this.notificationService.success('Diário fechado com sucesso!');
        this.preencherModelo(data);
      })
      .finally(() => this.loadingService.endLoading());
  }

  validarAssertVoltar(assert: SmcAssertComponent) {
    if (this.maximizar) {
      this.maximizar = false;
    } else {
      if (this.formNotas.dirty) {
        assert.confirmCallback = () => (this.document.location.href = '../');
        assert.showMessage('Existem dados não gravados, deseja prosseguir?', 'Confirmar saída do lançamento de notas');
      } else {
        this.document.location.href = '../';
      }
    }
  }

  emitirRelatorio() {
    window.open(
      `../APR/LancamentoNota/DiarioTurmaRelatorio?seqOrigemAvaliacao=${this.model.seqOrigemAvaliacao}`,
      '_blank'
    );
  }
}
