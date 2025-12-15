import { Injectable } from '@angular/core';
import { EventoAulaTurmaModel } from '../moldels/evento-aula-turma.model';
import { EventoAulaTurmaCabecalhoModel } from '../moldels/evento-aula-turma-cabecalho.model';
import { EventoAulaDivisaoTurmaModel } from '../moldels/evento-aula-divisao-turma.model';
import { EventoAulaAgendamentoHorarioModel } from '../moldels/evento-aula-agendamento-horario.model';
import { EventoAulaAgendamentoTabelaHorarioModel } from '../moldels/evento-aula-agendamento-tabela-horario.model';
import { SmcKeyValueModel } from 'projects/shared/models/smc-key-value.model';
import { PoCheckboxGroupOption } from '@po-ui/ng-components';
import { EventoAulaFeriadoModel } from '../moldels/evento-aula-feriado.model';
import { SmcTreeViewItem } from 'projects/shared/components/smc-tree-view/smc-tree-view-item/smc-tree-view-item.interface';
import { EventoAulaModel } from './../moldels/evento-aula.model';
import { SmcTokensSeguranca } from './../../../../../../shared/models/smc-tokens-seguranca.moldes';
import { EventoAulaColaboradorModel } from './../moldels/evento-aula-colaborador.model';
import { EventoAulaTurmaCabecalhoAssociarProfessorModel } from './../moldels/evento-aula-turma-cabecalho-associar-professor.model';

@Injectable({
  providedIn: 'root',
})
export class EventoAulaDataService {
  /**
   * @description Dados da turma para toda a tela
   */
  private _dadosTurma: EventoAulaTurmaModel;

  set dadosTurma(dadosTurma: EventoAulaTurmaModel) {
    this._dadosTurma = dadosTurma;
  }

  get dadosTurma() {
    return this._dadosTurma;
  }

  /**
   * @description Tabela de dados dos horários AGD
   */
  private _tabelaHorarioAgd: EventoAulaAgendamentoTabelaHorarioModel;

  set tabelaHorarioAgd(tabelaAgd: EventoAulaAgendamentoTabelaHorarioModel) {
    this._tabelaHorarioAgd = tabelaAgd;
  }

  get tabelaHorarioAgd() {
    return this._tabelaHorarioAgd;
  }

  /**
   * @description Data source do tipo de ocorrencia
   */
  private _dataSourceTipoOcorrencia: SmcKeyValueModel[];

  set dataSourceTipoOcorrencia(tiposOcorencia: SmcKeyValueModel[]) {
    this._dataSourceTipoOcorrencia = tiposOcorencia;
  }

  get dataSourceTipoOcorrencia() {
    return this._dataSourceTipoOcorrencia;
  }

  /**
   * @description Data source dias da semana vindos do AGD
   */
  private _dataSourceDiasSemana: PoCheckboxGroupOption[];

  set dataSourceDiasSemana(diasSemana: PoCheckboxGroupOption[]) {
    this._dataSourceDiasSemana = diasSemana;
  }

  get dataSourceDiasSemana() {
    return this._dataSourceDiasSemana;
  }

  /**
   * @description Data source turnos vindos do AGD
   */
  private _dataSourceTurno: SmcKeyValueModel[];

  set dataSourceTurno(turnos: SmcKeyValueModel[]) {
    this._dataSourceTurno = turnos;
  }

  get dataSourceTurno() {
    return this._dataSourceTurno;
  }

  /**
   * @description Data source Tipo de incio ocorrencia vindos do AGD
   */
  private _dataSourceTipoInicioOcorrencia: SmcKeyValueModel[];

  set dataSourceTipoInicioOcorrencia(tiposInicioOcorrencia: SmcKeyValueModel[]) {
    this._dataSourceTipoInicioOcorrencia = tiposInicioOcorrencia;
  }

  get dataSourceTipoInicioOcorrencia() {
    return this._dataSourceTipoInicioOcorrencia;
  }

  /**
   * @description Datasource dos colaboradores elegiveis apra turma segundo a regra RN_GRD_009 - Lista de professores
   */
  private _dataSourceColaboradores: SmcKeyValueModel[];

  set dataSourceColaboradores(colaboradoes: SmcKeyValueModel[]) {
    this._dataSourceColaboradores = colaboradoes;
  }

  get dataSourceColaboradores() {
    return this._dataSourceColaboradores;
  }

  /**
   * @description Datasource dos locais cadastrados no AGD
   */
  private _dataSourceLocal: SmcTreeViewItem[];

  set dataSourceLocal(lacais: SmcTreeViewItem[]) {
    this._dataSourceLocal = lacais;
  }

  get dataSourceLocal() {
    return this._dataSourceLocal;
  }

  /**
   * @description Datasource de feriados periodo letivo
   */
  private _dataSourceFeriados: EventoAulaFeriadoModel[];

  set dataSourceFeriados(feriados: EventoAulaFeriadoModel[]) {
    this._dataSourceFeriados = feriados;
  }

  get dataSourceFeriados() {
    return this._dataSourceFeriados;
  }

  private _turmaIntegradaSEF: boolean;

  /**
   * @description Flag que defini que a turma está integrada com o SEF.
   * Esse flag é processado de forma asincrona após abertura da tela.
   */
  set turmaIntegradaSEF(value: boolean) {
    this._turmaIntegradaSEF = value;
  }

  get turmaIntegradaSEF(): boolean {
    return this._turmaIntegradaSEF;
  }

  private _eventosProcessando: EventoAulaModel[] = [];
  /**
   * @description Eventos que estrão sendo processados
   */
  set eventoProcessando(values: EventoAulaModel[]) {
    this._eventosProcessando = values;
  }

  get eventoProcessando() {
    return this._eventosProcessando;
  }

  private _coresDivisoesTurma: SmcKeyValueModel[] = [];

  /**
   * @description Cores que foram atribuidas as divisoes de turma
   */
  set coresDivisoesTurma(value: SmcKeyValueModel[]) {
    this._coresDivisoesTurma = value;
  }

  get coresDivisoesTurma() {
    return this._coresDivisoesTurma;
  }

  private _tokensSeguranca: SmcTokensSeguranca[] = [];

  /**
   * @description Tokens de segurança permitido para o usuario
   */
  set tokensSeguranca(value: SmcTokensSeguranca[]) {
    this._tokensSeguranca = value;
  }

  get tokensSeguranca() {
    return this._tokensSeguranca;
  }

  private _colaboradoresTurma: EventoAulaColaboradorModel[] = [];

  /**
   * @description Colaboradores da turma
   */
  set colaboradoresTurma(value: EventoAulaColaboradorModel[]) {
    this._colaboradoresTurma = value;
  }

  get colaboradoresTurma() {
    return this._colaboradoresTurma;
  }

  private _dadosCabecalhoAssociarProfessor: EventoAulaTurmaCabecalhoAssociarProfessorModel;

  /**
   * @description Dados cabecalho associar professor responsável turma
   */
     set dadosCabecalhoAssociarProfessor(value: EventoAulaTurmaCabecalhoAssociarProfessorModel) {
      this._dadosCabecalhoAssociarProfessor = value;
    }

    get dadosCabecalhoAssociarProfessor() {
      return this._dadosCabecalhoAssociarProfessor;
    }

  constructor() {}
}
