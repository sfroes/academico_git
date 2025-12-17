import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import moment from 'moment';
import { distinctArray, isNullOrEmpty } from 'projects/shared/utils/util';
import { environment } from 'projects/smc-sga-professor/src/environments/environment';
import { ApuracaoFrequenciaAlunoModel } from '../models/apuracao-frequencia-aluno.model';
import { ApuracaoFrequenciaApuracaoModel } from '../models/apuracao-frequencia-apuracao.model';
import { ApuracaoFrequenciaAulaModel } from '../models/apuracao-frequencia-aula.model';
import { Frequencia } from '../models/apuracao-frequencia-types.model';
import { ApuracaoFrequenciaModel } from '../models/apuracao-frequencia.model';
import { ApuracaoFrequenciaDataService } from './apuracao-frequencia-data.service';

@Injectable({
  providedIn: 'root',
})
export class ApuracaoFrequenciaService {
  private baseUrl: string;

  constructor(private http: HttpClient, private dataService: ApuracaoFrequenciaDataService, private fb: FormBuilder) {
    this.baseUrl = `${environment.frontUrl}/APR/ApuracaoFrequenciaGrade/`;
  }

  buscarLancamentoFrequencia(seqOrigemAvaliacao: string): Promise<ApuracaoFrequenciaModel> {
    const url = `${this.baseUrl}BuscarLancamentoFrequencia?SeqOrigemAvaliacao=${seqOrigemAvaliacao}`;
    return this.http.get<ApuracaoFrequenciaModel>(url).toPromise();
  }

  buscarHorarioLimiteTurno(seqOrigemAvaliacao: string): Promise<string> {
    const url = `${this.baseUrl}buscarHorarioLimiteTurno?SeqOrigemAvaliacao=${seqOrigemAvaliacao}`;
    return this.http.get<string>(url).toPromise();
  }

  salvarLancamentoFrequencia(data: ApuracaoFrequenciaApuracaoModel[]): Promise<void> {
    const url = `${this.baseUrl}SalvarLancamentoFrequencia`;
    return this.http.post<void>(url, data).toPromise();
  }

  async carregarDados(seqOrigemAvaliacao: string): Promise<void> {
    this.buscarHorarioLimiteTurno(seqOrigemAvaliacao).then(horario =>
      this.dataService.horarioLimiteTurno$.next(horario)
    );
    const dados = await this.buscarLancamentoFrequencia(seqOrigemAvaliacao);
    dados.dataLimite = moment(dados.dataLimite).toDate();
    dados.alunos.forEach(f => {
      f.mostrar = true;
      f.apuracoes.forEach(fa => (fa.mostrar = false));
    });
    dados.aulas.forEach(f => {
      const mData = moment(f.data);
      f.data = mData.toDate();
      f.dataFormatada = mData.format('YYYYMMDD');
      f.mostrar = false;
      f.situacaoApuracaoFrequenciaOriginal = f.situacaoApuracaoFrequencia;
    });
    dados.dias = distinctArray(dados.aulas.map(m => m.dataFormatada))
      .sort()
      .map(m => ({
        data: moment(m, 'YYYYMMDD').toDate(),
        descricaoFormatada: moment(m, 'YYYYMMDD').format('DD/MM/YYYY'),
        mostrar: false,
        aulas: dados.aulas.filter(f => f.dataFormatada === m).sort((a, b) => a.horaInicio.localeCompare(b.horaInicio)),
      }));
    this.dataService.dataHoraServidor = await this.buscarDataHoraServido();
    this.criarAlunos(dados.alunos, dados.cargaHoraria);
    this.dataService.model$.next(dados);
  }

  async salvarDados() {
    const data: ApuracaoFrequenciaApuracaoModel[] = [];
    const alunos = this.dataService.form.controls.alunos as FormArray;
    const seqsEventoAulaApuracao = this.dataService.modelSnapshot.aulas
      .filter(f => f.situacaoApuracaoFrequencia === 'Em apuração')
      .map(m => m.seqEventoAula);
    this.atualizarFrequenciaRegistrosPendentes();
    alunos.controls
      .filter(f => f.dirty)
      .forEach(f => {
        const aluno = f as FormGroup;
        const apuracoes = aluno.controls.apuracoes as FormArray;
        apuracoes.controls
          .filter(f => f.dirty)
          .forEach(f => {
            const apuracao = f as FormGroup;
            if (seqsEventoAulaApuracao.includes(apuracao.controls.seqEventoAula.value)) {
              data.push(apuracao.value);
            }
          });
      });
    await this.salvarLancamentoFrequencia(data);
  }

  atualizarSituacaoFrequenciaAula(aula: ApuracaoFrequenciaAulaModel, frequencia?: Frequencia) {
    let atualizar = this.dataService.emApuracao$.value;
    let cancelar = this.dataService.emCancelamento$.value;
    if (!isNullOrEmpty(frequencia) && aula.situacaoApuracaoFrequencia !== 'Em apuração') {
      if (aula.situacaoApuracaoFrequencia === 'Em cancelamento') {
        cancelar--;
      }

      if (frequencia !== 'Ausência abonada') {
        atualizar++;
        aula.situacaoApuracaoFrequencia = 'Em apuração';
      }
    } else if (isNullOrEmpty(frequencia)) {
      if (aula.situacaoApuracaoFrequencia === 'Em apuração') {
        atualizar--;
        aula.situacaoApuracaoFrequencia = 'Não apurada';
        if (aula.situacaoApuracaoFrequenciaOriginal === 'Executada') {
          cancelar++;
          aula.situacaoApuracaoFrequencia = 'Em cancelamento';
        }
      } else if (aula.situacaoApuracaoFrequencia === 'Executada') {
        cancelar++;
        aula.situacaoApuracaoFrequencia = 'Em cancelamento';
      }
    }
    this.dataService.emApuracao$.next(atualizar);
    this.dataService.emCancelamento$.next(cancelar);
  }

  atualizarFrequenciaRegistrosPendentes() {
    const seqEventosAulaApuracao = this.dataService.modelSnapshot.aulas
      .filter(f => f.situacaoApuracaoFrequencia === 'Em apuração')
      .map(m => m.seqEventoAula);
    const alunos = this.dataService.form.controls.alunos as FormArray;
    const presente: Frequencia = 'Presente';
    alunos.controls.forEach(f => {
      const aluno = f as FormGroup;
      const alunoModel = this.dataService.modelSnapshot.alunos.find(
        f => f.seqAlunoHistoricoCicloLetivo == aluno.controls.seqAlunoHistoricoCicloLetivo.value
      );
      const apuracoes = aluno.controls.apuracoes as FormArray;
      apuracoes.controls
        .map(m => m as FormGroup)
        .filter(f => seqEventosAulaApuracao.includes(f.controls.seqEventoAula.value))
        .forEach(f => {
          const apuracao = f as FormGroup;
          const apuracaoModel = alunoModel.apuracoes.find(
            f => f.seqEventoAula == apuracao.controls.seqEventoAula.value
          );
          if (isNullOrEmpty(apuracao.controls.frequencia.value) && isNullOrEmpty(apuracaoModel.frequencia)) {
            apuracao.controls.frequencia.setValue(presente);
            apuracao.controls.frequencia.markAsDirty();
          }
        });
    });
  }

  buscarDataHoraServido() {
    const url = `${this.baseUrl}BuscarDataHoraServidor`;
    return this.http.get<Date>(url).toPromise();
  }

  validarDataLimiteApuracao(aula: ApuracaoFrequenciaAulaModel): boolean {
    let dataLimite = moment(aula.dataLimiteApuracaoFrequencia);
    // Data limite alteração
    if (aula.dataPrimeiraApuracaoFrequencia) {
      dataLimite = moment(aula.dataPrimeiraApuracaoFrequencia).add(
        this.dataService.model$.value.quantidadeMinutosPrazoAlteracaoFrequencia,
        'minute'
      );
    }
    const dataAtual = moment(this.dataService.dataHoraServidor);
    return dataLimite > dataAtual;
  }

  validarColisaoProfessor(aula: ApuracaoFrequenciaAulaModel): boolean {
    let retorno = false;
    const professorCorrete = this.dataService.model$.value.usuarioAutenticado;
    const professorAula = aula.usuarioPrimeiraApuracaoFrequencia;
    const dataLimiteValida = this.validarDataLimiteApuracao(aula);
    if (dataLimiteValida && professorCorrete !== professorAula && professorAula !== null) {
      retorno = true;
    }
    return retorno;
  }

  private criarAlunos(model: ApuracaoFrequenciaAlunoModel[], cargaHoraria: number) {
    const alunos = this.dataService.form.controls.alunos as FormArray;
    alunos.clear();
    model.forEach(f => {
      //const faltas = f.apuracoes.filter(f => f.frequencia === 'Ausente').length;
      const faltas = this.somarFaltas(f.apuracoes);
      const percentual = Math.trunc((faltas / cargaHoraria) * 100);
      alunos.push(
        this.fb.group({
          seqAlunoHistoricoCicloLetivo: [f.seqAlunoHistoricoCicloLetivo],
          apuracoes: this.criarApuracores(f.seqAlunoHistoricoCicloLetivo, f.apuracoes),
          total: [faltas],
          percentual: [percentual],
        })
      );
    });
  }

  private criarApuracores(seqAlunoHistoricoCicloLetivo: string, model: ApuracaoFrequenciaApuracaoModel[]) {
    const apuracoes = this.fb.array([]);
    model.reverse().forEach(f => {
      apuracoes.push(
        this.fb.group({
          seq: [f.seq],
          seqEventoAula: [f.seqEventoAula],
          seqAlunoHistoricoCicloLetivo: [seqAlunoHistoricoCicloLetivo],
          frequencia: [f.frequencia],
          observacao: [f.observacao],
          dataObservacao: [f.dataObservacao],
          ocorrenciaFrequencia: [f.ocorrenciaFrequencia],
          descricaoTipoMensagem: [f.descricaoTipoMensagem],
        })
      );
    });
    return apuracoes;
  }

  private somarFaltas(apuracoes: ApuracaoFrequenciaApuracaoModel[]): number {
    const faltasNaoAbonadas = apuracoes.filter(
      f => f.frequencia === 'Ausente' && f.ocorrenciaFrequencia !== 'Abono/Retificação'
    );
    const presencasSancionadas = apuracoes.filter(
      f => f.frequencia === 'Presente' && f.ocorrenciaFrequencia === 'Sanção'
    );
    return faltasNaoAbonadas.length + presencasSancionadas.length;
  }
}
