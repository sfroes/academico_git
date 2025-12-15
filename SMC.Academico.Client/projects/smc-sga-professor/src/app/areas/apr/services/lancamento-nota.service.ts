import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AbstractControl } from '@angular/forms';
import { environment } from 'projects/smc-sga-professor/src/environments/environment';
import { of } from 'rxjs';
import { LancamentoNotaAlunoApuracaoModel } from '../lancamento-nota/models/lancamento-nota-aluno-apuracao.model';
import { LancamentoNotaAlunoTotaisDivisaoTurmaModel } from '../lancamento-nota/models/lancamento-nota-aluno-totais-divisao-turma.model';
import { LancamentoNotaAlunoTotaisTurmaModel } from '../lancamento-nota/models/lancamento-nota-aluno-totais-turma.model';
import { LancamentoNotaDiario } from '../lancamento-nota/models/lancamento-nota-fechamento-diario';
import { LancamentoNotaRetornoModel } from '../lancamento-nota/models/lancamento-nota-retorno.model';
import { LancamentoNotaSituacaoFinalModel } from '../lancamento-nota/models/lancamento-nota-situacao-final.model';
import { LancamentoNotaModel } from '../lancamento-nota/models/lancamento-nota.model';
import { LancamentoNotaMapService } from '../lancamento-nota/services/lancamento-nota-map.service';
import { SmcTokensSeguranca } from './../../../../../../shared/models/smc-tokens-seguranca.moldes';

@Injectable({
  providedIn: 'root',
})
export class LancamentoNotaService {
  private baseUrl: string;

  constructor(private http: HttpClient, private lancamentoNotaMapService: LancamentoNotaMapService) {
    this.baseUrl = `${environment.frontUrl}/APR/LancamentoNota/`;
  }

  buscarLancamento(seqOrigemAvaliacao: string) {
    return this.http.get<LancamentoNotaModel>(
      `${this.baseUrl}BuscarLancamentosAvaliacao?SeqOrigemAvaliacao=${seqOrigemAvaliacao}`
    );
  }

  salvarLancamento(lancamentos: LancamentoNotaRetornoModel) {
    return this.http.post<LancamentoNotaModel>(`${this.baseUrl}SalvarLancamentosAvaliacao`, lancamentos);
  }

  fecharDiario(model: LancamentoNotaDiario) {
    return this.http.post<LancamentoNotaModel>(`${this.baseUrl}FecharDiario`, model);
  }

  excluirHistoricoEscolar(seqAlunoHistorico: string, seqOrigemAvaliacao: string) {
    return this.http.get<LancamentoNotaModel>(
      `${this.baseUrl}ExcluirHistoricoEscolar?SeqAlunoHistorico=${seqAlunoHistorico}&seqOrigemAvaliacao=${seqOrigemAvaliacao}`
    );
  }

  validarApuracaoModificada(apuracaoModel: LancamentoNotaAlunoApuracaoModel, apuracaoForm: AbstractControl) {
    return (
      this.lancamentoNotaMapService.validarApuracaoModificadaComValor(apuracaoModel, apuracaoForm) ||
      this.lancamentoNotaMapService.validarApuracaoModificadaSemValor(apuracaoForm)
    );
  }

  calcularSituacaoFinal(seqOrigemAvaliacao: string, seqAlunoHistorico: string, notaTotal: number) {
    return this.http.get<LancamentoNotaSituacaoFinalModel>(
      `${this.baseUrl}CalcularSituacaoFinal?SeqOrigemAvaliacao=${seqOrigemAvaliacao}&seqAlunoHistorico=${seqAlunoHistorico}&notaTotal=${notaTotal}`
    );
  }

  calcularTotalDivisaoTurma(seqOrigemAvaliacao: string, seqAlunoHistorico: string, formApuacoes: AbstractControl) {
    const modelo = this.lancamentoNotaMapService.mapearModeloCalculoTotal(
      seqOrigemAvaliacao,
      seqAlunoHistorico,
      null,
      formApuacoes
    );
    if (environment.localSum) {
      let result: number = null;
      modelo.apuracoes.forEach(apuracao => {
        if (apuracao.nota !== null) {
          if (result === null) {
            result = 0;
          }
          result += +apuracao.nota.toString().replace(',', '.');
        }
      });
      return of(result);
    }
    return this.http.post<number>(`${this.baseUrl}CalcularTotalDivisaoTurma`, modelo);
  }

  calcularTotalTurma(
    seqOrigemAvaliacao: string,
    seqAlunoHistorico: string,
    totalParcial: number,
    formApuacoes: AbstractControl
  ) {
    const modelo = this.lancamentoNotaMapService.mapearModeloCalculoTotal(
      seqOrigemAvaliacao,
      seqAlunoHistorico,
      totalParcial,
      formApuacoes
    );
    return this.http.post<number>(`${this.baseUrl}CalcularTotalTurma`, modelo);
  }

  calcularTotalParcial(seqOrigemAvaliacao: string, seqAlunoHistorico: string) {
    return this.http.get<string>(
      `${this.baseUrl}CalcularTotalParcial?SeqOrigemAvaliacao=${seqOrigemAvaliacao}&seqAlunoHistorico=${seqAlunoHistorico}`
    );
  }

  calcularTotaisTurma(seqOrigemAvaliacao: string, permiteAlunoSemNota: boolean, seqsAlunoHistorico: string[]) {
    const modelo = {
      seqOrigemAvaliacao,
      permiteAlunoSemNota,
      seqsAlunoHistorico,
    };
    return this.http.post<LancamentoNotaAlunoTotaisTurmaModel[]>(`${this.baseUrl}CalcularTotaisTurma`, modelo);
  }

  calcularTotaisDivisaoTurma(seqOrigemAvaliacao: string, seqsAlunoHistorico: string[]) {
    const modelo = {
      seqOrigemAvaliacao,
      seqsAlunoHistorico,
    };
    return this.http.post<LancamentoNotaAlunoTotaisDivisaoTurmaModel[]>(
      `${this.baseUrl}CalcularTotaisDivisaoTurma`,
      modelo
    );
  }

  buscarTokensSeguranca() {
    const url = `${this.baseUrl}BuscarTokensSeguranca`;
    return this.http.get<SmcTokensSeguranca[]>(url).toPromise();
  }
}
