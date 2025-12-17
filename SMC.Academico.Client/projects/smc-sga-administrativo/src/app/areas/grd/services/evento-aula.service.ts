import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { PoCheckboxGroupOption } from '@po-ui/ng-components';
import moment from 'moment';
import { BehaviorSubject, Subject } from 'rxjs';
import { map } from 'rxjs/operators';
import { v4 as uuid } from 'uuid';
import { SmcKeyValueModel } from '../../../../../../shared/models/smc-key-value.model';
import { EventoAulaAgendamentoTabelaHorarioModel } from '../moldels/evento-aula-agendamento-tabela-horario.model';
import { EventoAulaFeriadoModel } from '../moldels/evento-aula-feriado.model';
import { EventoAulaSimulacaoModel } from '../moldels/evento-aula-simulacao.model';
import { EventoAulaTurmaModel } from '../moldels/evento-aula-turma.model';
import { EventoAulaModel } from '../moldels/evento-aula.model';
import { EventoAulaDataService } from './evento-aula-data.service';
import { SmcTreeViewService } from './../../../../../../shared/components/smc-tree-view/services/smc-tree-view.service';
import { EventoAulaLocalModel } from './../moldels/evento-aula-local.model';
import { EventoAulaDivisaoTurmaModel } from './../moldels/evento-aula-divisao-turma.model';
import { isNullOrEmpty } from 'projects/shared/utils/util';
import { EventoAulaValidacaoColisaoColaboradorModel } from '../moldels/evento-aula-validacao-colisao-colaborador.model';
import { environment } from 'projects/smc-sga-administrativo/src/environments/environment';
import { EventoAulaAgendamentoProfessorModel } from '../moldels/evento-aula-agendamento-professor.model';
import { SmcTokensSeguranca } from './../../../../../../shared/models/smc-tokens-seguranca.moldes';
import { EventoAulaColaboradorModel } from './../moldels/evento-aula-colaborador.model';
import { EventoAulaTurmaCabecalhoAssociarProfessorModel } from '../moldels/evento-aula-turma-cabecalho-associar-professor.model';

@Injectable({
  providedIn: 'root',
})
export class EventoAulaService {
  private _refresh = new Subject<void>();
  get refresh() {
    return this._refresh;
  }

  private _loadingCount = 0;
  get loadingCount() {
    return this._loadingCount;
  }
  set loadingCount(value: number) {
    this._loadingCount = value;
    this._loadingDataSource.next(value != 0);
  }

  private _loadingDataSource = new BehaviorSubject(false);
  get loadingDataSource() {
    return this._loadingDataSource;
  }

  constructor(
    private http: HttpClient,
    private sanitizer: DomSanitizer,
    private dataService: EventoAulaDataService,
    private smcTreeViewService: SmcTreeViewService
  ) {}

  buscarDetalheDivisaoTurma(seqDivisaoTurma: string) {
    const url = `../GRD/DetalheDivisaoTurmaGrade/ConsultaDetalheDivisaoTurmaGradeAngular?seqDivisaoTurma=${seqDivisaoTurma}`;
    return this.http
      .get(url, { responseType: 'text' })
      .pipe(map(result => this.sanitizer.bypassSecurityTrustHtml(result as string)))
      .toPromise();
  }

  buscarDetalheComponenteCurricular(seqComponenteCurricular: string) {
    const url = `../CUR/ComponenteCurricular/VerDetalhesPartial?seq=${seqComponenteCurricular}`;
    return this.http
      .get(url, { responseType: 'text' })
      .pipe(map(result => this.sanitizer.bypassSecurityTrustHtml(result as string)))
      .toPromise();
  }

  buscarEventosTurma(seqTurma: string) {
    const url = `../GRD/EventoAula/BuscarEventosTurma`;
    const data = { seqTurma: seqTurma };
    return this.http
      .post<EventoAulaTurmaModel>(url, data)
      .pipe(
        map(data => {
          data.eventoAulaDivisoesTurma?.forEach(divisao => {
            divisao.eventoAulas?.forEach(evento => {
              evento.data = moment(evento.data).toDate();
            });
          });
          return data;
        })
      )
      .toPromise();
  }

  buscarDadosCabecalhoAssociarProfessor(seqTurma){
    const url = `../GRD/EventoAula/BuscarDadosCabecalhoAssociarProfessor`;
    const data = { seqTurma: seqTurma };
    return this.http.post<EventoAulaTurmaCabecalhoAssociarProfessorModel>(url, data).toPromise();
  }

  buscarTabelaHorarioAGD() {
    const data = {
      seqAgendaTurma: this.dataService.dadosTurma.eventoAulaTurmaCabecalho.seqAgendaTurma,
      dataInicioPeriodoLetivo: this.dataService.dadosTurma.eventoAulaTurmaCabecalho.inicioPeriodoLetivo,
      dataFimPeriodoLetivo: this.dataService.dadosTurma.eventoAulaTurmaCabecalho.fimPeriodoLetivo,
    };
    const url = `../GRD/EventoAula/BuscarTabelaHorarioAGD`;
    return this.http.post<EventoAulaAgendamentoTabelaHorarioModel>(url, data).toPromise();
  }

  buscarTipoRecorrenciaAGD() {
    const url = `../GRD/EventoAula/BuscarTipoRecorrenciaAGD`;
    return this.http.get<SmcKeyValueModel[]>(url).toPromise();
  }

  buscarTipoInicioRecorrenciaAGD() {
    const url = `../GRD/EventoAula/BuscarTipoInicioRecorrenciaAGD`;
    return this.http.get<SmcKeyValueModel[]>(url).toPromise();
  }

  buscarDiasSemanaAGD() {
    const url = `../GRD/EventoAula/BuscarDiaSemanaAGD`;
    return this.http.get<PoCheckboxGroupOption[]>(url).toPromise();
  }

  buscarTurnoAGD() {
    const url = `../GRD/EventoAula/BuscarTurnoAGD`;
    return this.http.get<SmcKeyValueModel[]>(url).toPromise();
  }

  buscarLocaisAGD() {
    const data = {
      codigoUnidadeSeo: this.dataService.dadosTurma.eventoAulaTurmaCabecalho.codigoUnidadeSeo,
      seqAgendaTurma: this.dataService.dadosTurma.eventoAulaTurmaCabecalho.seqAgendaTurma,
    };
    const url = `../GRD/EventoAula/BuscarLocaisAGD`;
    return this.http.post<EventoAulaLocalModel>(url, data).toPromise();
  }

  buscarDescricaoLocalAGD(codigoLocalSEF: number) {
    const url = `../GRD/EventoAula/BuscarLocalAGD?codigoLocalSEF=${codigoLocalSEF}`;
    return this.http.get<string>(url).toPromise();
  }

  buscarColaboradoresTurma(seqTurma: string, seqCursoOfertaLocalidade) {
    const url = `../GRD/EventoAula/BuscarColaboradores`;
    const data = { seqTurma: seqTurma, seqCursoOfertaLocalidade: seqCursoOfertaLocalidade };
    return this.http.post<EventoAulaColaboradorModel[]>(url, data).toPromise();
  }

  buscarFeriados() {
    const url = `../GRD/EventoAula/BuscarFeriados`;
    const data = {
      codigoUnidadeSeo: this.dataService.dadosTurma.eventoAulaTurmaCabecalho.codigoUnidadeSeo,
      dataInicio: this.dataService.dadosTurma.eventoAulaTurmaCabecalho.inicioPeriodoLetivo,
      dataFim: this.dataService.dadosTurma.eventoAulaTurmaCabecalho.fimPeriodoLetivo,
    };
    return this.http.post<EventoAulaFeriadoModel[]>(url, data).toPromise();
  }

  buscarTokensSeguranca() {
    const url = `../GRD/EventoAula/BuscarTokensSeguranca`;
    return this.http.get<SmcTokensSeguranca[]>(url).toPromise();
  }

  preencherDataSources() {
    this.loadingCount += 7;
    this.buscarDadosCabecalhoAssociarProfessor(this.dataService.dadosTurma.eventoAulaTurmaCabecalho.seqTurma).then(result => {
      this.dataService.dadosCabecalhoAssociarProfessor = result;
      this._loadingCount--;
    });
    this.buscarFeriados().then(result => {
      this.dataService.dataSourceFeriados = result.map(m => ({
        ...m,
        dataInicio: moment(m.dataInicio, 'YYYY-MM-DD').toDate(),
        dataFim: moment(m.dataFim, 'YYYY-MM-DD').toDate(),
      }));
      this.refresh.next();
    });
    this.buscarTipoRecorrenciaAGD().then(result => {
      this.dataService.dataSourceTipoOcorrencia = result;
      this.loadingCount--;
    });
    this.buscarTipoInicioRecorrenciaAGD().then(result => {
      this.dataService.dataSourceTipoInicioOcorrencia = result;
      this.loadingCount--;
    });
    this.buscarDiasSemanaAGD().then(result => {
      this.dataService.dataSourceDiasSemana = result;
      this.loadingCount--;
    });
    this.buscarTurnoAGD().then(result => {
      this.dataService.dataSourceTurno = result;
      this.loadingCount--;
    });
    this.buscarColaboradoresTurma(
      this.dataService.dadosTurma.eventoAulaTurmaCabecalho.seqTurma,
      this.dataService.dadosTurma.eventoAulaTurmaCabecalho.seqCursoOfertaLocalidade
    ).then(result => {
      this.dataService.colaboradoresTurma = result;
      this.dataService.dataSourceColaboradores = result.map<SmcKeyValueModel>(m => {
        return { key: m.seq, value: m.nomeFormatado };
      });
      this.loadingCount--;
    });
    this.buscarTokensSeguranca().then(result => {
      this.dataService.tokensSeguranca = result;
      this.loadingCount--;
    });
    if (!isNullOrEmpty(this.dataService.dadosTurma.eventoAulaTurmaCabecalho.seqAgendaTurma)) {
      this.loadingCount += 2;
      this.buscarTabelaHorarioAGD().then(
        result => {
          this.dataService.tabelaHorarioAgd = result;
          this.loadingCount--;
        },
        err => {
          this.dataService.dadosTurma.eventoAulaTurmaCabecalho.mensagemFalha = err.error.error.message;
          this.dataService.dadosTurma.eventoAulaTurmaCabecalho.somenteLeitura = true;
          this.loadingCount--;
        }
      );
      //Caso não exista divisão de turma desnecessario buscar locais
      if (this.dataService.dadosTurma.eventoAulaDivisoesTurma.length > 0) {
        this.buscarLocaisAGD().then(result => {
          this.dataService.dataSourceLocal = this.smcTreeViewService.mountItemsTree(
            result.locais,
            !environment.production
          );
          this.dataService.turmaIntegradaSEF = result.turmaIntegradaSEF;
          this.loadingCount--;
        });
      }
    }
  }

  gerarSimulacaoEventos(model: EventoAulaSimulacaoModel): EventoAulaModel[] {
    const eventosAula: EventoAulaModel[] = [];
    const tipoIncremento = model.idtTipoRecorrencia == 2 ? 'w' : 'M';
    const dadosDivisao = this.dataService.dadosTurma.eventoAulaDivisoesTurma.find(f => f.seq === model.seqDivisaoTurma);

    if (dadosDivisao.tipoPulaFeriado === 'Pula Conjugado' && dadosDivisao.tipoDistribuicaoAula === 'Quinzenal') {
      return this.gerarSimulacaoEventosConjulgado(model);
    }

    const guids = model.seqsHorarios.map(m => ({ key: m, value: uuid() as string }));

    model.seqsHorarios.forEach(f => {
      const horario = this.dataService.tabelaHorarioAgd.horarios.find(bt => bt.seq === f);
      const guid = guids.find(fg => f == fg.key).value;
      let novoComeco = moment(`${model.comeca} ${horario.horaInicio}`);
      let novoFim = moment(`${model.comeca} ${horario.horaFim}`);
      if (tipoIncremento === 'w') {
        // Descola o começo para o primeiro dia semana dentro do intervalo (considerando o enum do agd)
        for (let index = 0; +horario.seqDiaSemana != 2 ** novoComeco.day() && index < 31; index++) {
          novoComeco.add(1, 'd');
          novoFim.add(1, 'd');
        }
      } else {
        novoComeco = this.proximaOcorrenciaMes(novoComeco, model.idtTipoInicioRecorrencia, +horario.seqDiaSemana);
        novoFim = this.proximaOcorrenciaMes(novoFim, model.idtTipoInicioRecorrencia, +horario.seqDiaSemana);
      }
      do {
        //Caso não seja permitido criar evento por causa de feriado segue para o próximo evento
        if (this.permitirEventoValidandoFeriado(novoComeco.toDate(), dadosDivisao)) {
          const dataSemHora = new Date(novoComeco.toDate().setHours(0, 0, 0));
          eventosAula.push({
            seqDivisaoTurma: model.seqDivisaoTurma,
            seqHorarioAgd: f,
            descricao: model.descricaoDivisao,
            codigoRecorrencia: guid,
            diaSemana: +horario.seqDiaSemana,
            diaSemanaFormatada: `${novoComeco.format('DD/MM/YYYY')} - ${horario.diaSemana}`,
            data: novoComeco.startOf('day').toDate(),
            horaInicio: horario.horaInicio,
            horaFim: horario.horaFim,
            local: model.local,
            feriado: this.validarFeriado(dataSemHora),
            codigoLocalSEF: model.codigoLocalSEF,
            colaboradores: this.ValidarVinculoProfessores(model.colaboradores, novoComeco),
          } as EventoAulaModel);
        }
        novoComeco.add(model.repetir, tipoIncremento);
        novoFim.add(model.repetir, tipoIncremento);
        if (tipoIncremento === 'M') {
          novoComeco = this.proximaOcorrenciaMes(novoComeco, model.idtTipoInicioRecorrencia, +horario.seqDiaSemana);
          novoFim = this.proximaOcorrenciaMes(novoFim, model.idtTipoInicioRecorrencia, +horario.seqDiaSemana);
        }
      } while (novoComeco >= moment(`${model.comeca} 00:00:00`) && novoComeco <= moment(`${model.termina} 23:59:59`));
    });
    //Caso tenha somente uma recorrencia remove o codigo de recorrencia para ficar padrão igual ao agendamento reduzido
    if (eventosAula.length === 1) {
      eventosAula[0].codigoRecorrencia = null;
    }
    //Valida se os eventos gerados estão dentro do periodo da aula
    const retorno = eventosAula.filter(f => {
      if (
        f.data >= moment(`${model.comeca} 00:00:00`).toDate() &&
        f.data <= moment(`${model.termina} 23:59:59`).toDate()
      )
        return true;
    });
    return retorno.sort((a, b) => a.data.getTime() - b.data.getTime());
  }

  gerarSimulacaoEventosConjulgado(model: EventoAulaSimulacaoModel): EventoAulaModel[] {
    if (!moment(model.comeca).isValid()) {
      throw 'model.comeca inválido';
    }
    if (!moment(model.termina).isValid()) {
      throw 'model.termina inválido';
    }

    const eventosAula: EventoAulaModel[] = [];
    const tipoIncremento = 'w';
    const dadosDivisao = this.dataService.dadosTurma.eventoAulaDivisoesTurma.find(f => f.seq === model.seqDivisaoTurma);

    const guids = model.seqsHorarios.map(m => ({ key: m, value: uuid() as string }));

    model.seqsHorarios.forEach(f => {
      const horario = this.dataService.tabelaHorarioAgd.horarios.find(bt => bt.seq === f);
      const guid = guids.find(fg => f == fg.key).value;
      let novoComeco = moment(`${model.comeca} ${horario.horaInicio}`);
      let novoFim = moment(`${model.comeca} ${horario.horaFim}`);
      let divisaoAtual = true;
      // Descola o começo para o primeiro dia semana dentro do intervalo (considerando o enum do agd)
      for (let index = 0; index < 31 && +horario.seqDiaSemana != 2 ** novoComeco.day(); index++) {
        novoComeco.add(1, 'd');
        novoFim.add(1, 'd');
      }
      do {
        //Caso não seja permitido criar evento por causa de feriado segue para o próximo evento
        const atualValido = this.permitirEventoValidandoFeriado(novoComeco.toDate(), dadosDivisao);
        if (atualValido && divisaoAtual) {
          const dataSemHora = new Date(novoComeco.toDate().setHours(0, 0, 0));
          eventosAula.push({
            seqDivisaoTurma: model.seqDivisaoTurma,
            seqHorarioAgd: f,
            descricao: model.descricaoDivisao,
            codigoRecorrencia: guid,
            diaSemana: +horario.seqDiaSemana,
            diaSemanaFormatada: `${novoComeco.format('DD/MM/YYYY')} - ${horario.diaSemana}`,
            data: novoComeco.startOf('day').toDate(),
            horaInicio: horario.horaInicio,
            horaFim: horario.horaFim,
            local: model.local,
            feriado: this.validarFeriado(dataSemHora),
            codigoLocalSEF: model.codigoLocalSEF,
            colaboradores: this.ValidarVinculoProfessores(model.colaboradores, novoComeco),
          } as EventoAulaModel);
          divisaoAtual = !divisaoAtual;
        } else if (atualValido && !divisaoAtual) {
          divisaoAtual = true;
        }
        novoComeco.add(1, tipoIncremento);
        novoFim.add(1, tipoIncremento);
      } while (novoComeco >= moment(`${model.comeca} 00:00:00`) && novoComeco <= moment(`${model.termina} 23:59:59`));
    });
    //Valida se os eventos gerados estão dentro do periodo da aula
    const retorno = eventosAula.filter(f => {
      if (
        f.data >= moment(`${model.comeca} 00:00:00`).toDate() &&
        f.data <= moment(`${model.termina} 23:59:59`).toDate()
      )
        return true;
    });
    return eventosAula.sort((a, b) => a.data.getTime() - b.data.getTime());
  }

  salvarEventos(model: EventoAulaModel[]) {
    const url = `../GRD/EventoAula/SalvarEventos`;
    return this.http.post(url, model).toPromise();
  }

  editarEventos(model: EventoAulaModel[], seqEventosExcluir: string[]) {
    const url = `../GRD/EventoAula/EditarEventos`;
    const data = { model, seqsEventosExcluir: seqEventosExcluir };
    return this.http.post(url, data).toPromise();
  }

  editarLocalEventos(codigoLocalSEF: string, local: string, seqEventos: string[]) {
    const url = `../GRD/EventoAula/EditarLocalEventos`;
    const data = { codigoLocalSEF, local, seqEventos };
    return this.http.post(url, data).toPromise();
  }

  editarColaboradoresEventos(
    colaboradores: EventoAulaAgendamentoProfessorModel[],
    seqEventos: string[],
    seqEventoTemplate: string,
    somenteColaborador: boolean
  ) {
    const url = `../GRD/EventoAula/EditarColaboradoresEventos`;
    const data = { colaboradores, seqEventos, seqEventoTemplate, somenteColaborador };
    return this.http.post(url, data).toPromise();
  }

  excluirEventos(seqsEventos: string[]) {
    const url = `../GRD/EventoAula/ExcluirEventos`;
    return this.http.post(url, seqsEventos).toPromise();
  }

  atualizarEventos() {
    const seqTurma = this.dataService.dadosTurma.eventoAulaTurmaCabecalho.seqTurma;
    const buscarEventosPromise = this.buscarEventosTurma(seqTurma);
    return buscarEventosPromise.then(data => {
      this.dataService.dadosTurma = data;
      const eventosRetornnados: EventoAulaModel[] = [];
      this.dataService.dadosTurma.eventoAulaDivisoesTurma.forEach(divisao => {
        divisao.eventoAulas.forEach(evento => eventosRetornnados.push(evento));
      });
      this.dataService.eventoProcessando = this.dataService.eventoProcessando.filter(
        ep =>
          !eventosRetornnados.some(
            er =>
              er.seqDivisaoTurma === ep.seqDivisaoTurma &&
              (er.diaSemanaFormatada === ep.diaSemanaFormatada || er.seq === ep.seq)
          )
      );
      this.refresh.next();
    });
  }

  validarColisao(model: EventoAulaValidacaoColisaoColaboradorModel[]) {
    const url = `../GRD/EventoAula/ValidarColisao`;
    return this.http.post<string>(url, model).toPromise();
  }

  registrarEventosProcessando(eventos: EventoAulaModel[]) {
    const divisao = this.dataService.dadosTurma.eventoAulaDivisoesTurma.find(f => f.seq === eventos[0].seqDivisaoTurma);
    const corDivisao = this.dataService.coresDivisoesTurma.find(f => f.key === divisao.seq);
    eventos.forEach(evento => {
      evento.grupoFormatado = divisao.grupoFormatado;
      evento.cor = corDivisao.value;
    });
    this.dataService.eventoProcessando = [...this.dataService.eventoProcessando, ...eventos];
    const seqsProcessando = this.dataService.eventoProcessando.map(m => m.seq);
    divisao.eventoAulas = divisao.eventoAulas.filter(f => !seqsProcessando.includes(f.seq));
    this.refresh.next();
  }

  cancelarEventosProcessando(eventos: EventoAulaModel[] | string[]) {
    if (!eventos || !eventos.length) {
      return;
    }
    this.dataService.eventoProcessando = this.dataService.eventoProcessando.filter(
      f => !eventos.some(s => s === f || s === f.seq)
    );
    this.atualizarEventos();
  }

  permitirEventoValidandoFeriado(data: Date, divisao: EventoAulaDivisaoTurmaModel): boolean {
    //Não pula feriado: Eventos serão criados inclusive nas datas de feriado.
    if (divisao.tipoPulaFeriado === 'Não Pula') {
      return true;
    }
    const dataSemHora = new Date(data);
    dataSemHora.setHours(0, 0, 0);
    const feriadoEncontrado = this.validarFeriado(dataSemHora);
    return !feriadoEncontrado;
  }

  validarFeriado(data: Date): boolean {
    return this.dataService.dataSourceFeriados.some(f => data >= f.dataInicio && data <= f.dataFim);
  }

  validarDiaUtil(data: Date, divisao: EventoAulaDivisaoTurmaModel): boolean {
    const diaSemana = data.getDay();
    if (diaSemana === 0) {
      return false;
    }
    if (!divisao.aulaSabado && diaSemana === 6) {
      return false;
    }
    return this.permitirEventoValidandoFeriado(data, divisao);
  }

  validarVinculoColaboradorPeriodo(seqColaborador: string, dataInicio: Date, dataFim: Date) {
    const url = `../GRD/EventoAula/ValidarVinculoColaboradorPeriodo`;
    const model = {
      seqColaborador,
      dataInicio,
      dataFim,
    };
    return this.http.post<boolean>(url, model);
  }

  valorPadrao(valor: any, padrao: any = '-'): any {
    return isNullOrEmpty(valor) ? padrao : valor;
  }

  validarColisaoHorarioAluno(model: EventoAulaModel[]) {
    const url = `../GRD/EventoAula/ValidarColisaoHorarioAluno`;
    return this.http.post<string[]>(url, model).toPromise();
  }

  validarColisaoHorarioSolicitacaoServico(model: EventoAulaModel[]) {
    const url = `../GRD/EventoAula/ValidarColisaoHorarioSolicitacaoServico`;
    return this.http.post<string[]>(url, model).toPromise();
  }

  AssociarProfessorResponsavel(model: any){
    const url = `../GRD/EventoAula/AssociarProfessorResponsavel`;
    return this.http.post(url, model).toPromise();
  }

  preencherDataSourceColaboradoresTurma(){
    this.buscarDadosCabecalhoAssociarProfessor(this.dataService.dadosTurma.eventoAulaTurmaCabecalho.seqTurma).then(result => {
      this.dataService.dadosCabecalhoAssociarProfessor = result;
    });
  }

  private proximaOcorrenciaMes(dataReferencia: moment.Moment, idtTipoInicioRecorrencia: number, idtDiaSemana: number) {
    // Clona a data para não alterar o parâmetro recebido
    const dataInicial = dataReferencia.clone();
    const diaSemana = Math.log2(idtDiaSemana);
    const diaMes = isNullOrEmpty(idtTipoInicioRecorrencia) ? 1 : Math.log2(idtTipoInicioRecorrencia);
    dataInicial.startOf('month').add(dataReferencia.hour(), 'h').add(dataReferencia.minute(), 'm');
    // Descola o começo para o primeiro dia semana dentro do intervalo (considerando o enum do agd)
    for (let index = 0; diaSemana !== dataInicial.day() && index < 31; index++) {
      dataInicial.add(1, 'd');
    }
    // Desloca para ocorrência do mes
    dataInicial.add(7 * diaMes, 'd');
    // Caso a "ultima/5ª" ocorrência não for no mes, retorna ao mes correto
    if (dataInicial.month() !== dataReferencia.month()) {
      dataInicial.add(-7, 'd');
    }
    // Clona novamente para mudar a "data inicial" desse moment
    return dataInicial.clone();
  }

  private ValidarVinculoProfessores(colacoradores: EventoAulaAgendamentoProfessorModel[], dataEvento: moment.Moment) {
    const retorno: EventoAulaAgendamentoProfessorModel[] = [];
    colacoradores.forEach(colaborador => {

      const vinculos = this.dataService.colaboradoresTurma.find(f => f.seq === colaborador.seqColaborador).vinculos;
      vinculos.forEach(vinculo => {
        const dataIncioVinculoProfessor = moment(
          vinculo.dataInicio
        );
        const dataFimVinculoProfessor = moment(
          vinculo.dataFim
        );
        if (
          dataEvento >= dataIncioVinculoProfessor &&
          (!dataFimVinculoProfessor.isValid() || dataEvento <= dataFimVinculoProfessor)
        ) {
          retorno.push(colaborador);
        }
      });
    });
    return [...new Set(retorno)];
  }

  private calcularCargaHorariaLancada() {
    this.dataService.dadosTurma.eventoAulaDivisoesTurma.forEach(divisao => {
      if (divisao.tipoPulaFeriado === 'Não Pula') {
        divisao.cargaHorariaLancada = divisao.eventoAulas.filter(f => !this.validarFeriado(f.data)).length;
      }
    });
  }
}
