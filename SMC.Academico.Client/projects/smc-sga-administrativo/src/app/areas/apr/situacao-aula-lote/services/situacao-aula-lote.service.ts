import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../../environments/environment';
import { SituacaoAulaLoteDataService } from './situacao-aula-lote-data.service';
import { PoSelectOption } from '@po-ui/ng-components';
import { SituacaoAulaLoteFiltroModel } from '../models/situacao-aula-lote-filtro.model';
import { SituacaoAulaLoteListaModel } from '../models/situacao-aula-lote-lista.model';
import { SmcTokensSeguranca } from 'projects/shared/models/smc-tokens-seguranca.moldes';

@Injectable({
  providedIn: 'root',
})
export class SituacaoAulaLoteService {
  private baseUrl: string;
  constructor(private http: HttpClient, dataService: SituacaoAulaLoteDataService) {
    this.baseUrl = `${environment.frontUrl}/APR/SituacaoAulaLote/`;
  }

  buscarDetalheDivisaoTurma(seqTurma: string): Promise<PoSelectOption[]> {
    const url = `${this.baseUrl}BuscarDivisoesTurma?seqturma=${seqTurma}`;
    return this.http.get<PoSelectOption[]>(url).toPromise();
  }

  buscarAulas(modelo: SituacaoAulaLoteFiltroModel): Promise<SituacaoAulaLoteListaModel[]> {
    const url = `${this.baseUrl}BuscarAulas`;
    return this.http.post<SituacaoAulaLoteListaModel[]>(url, modelo).toPromise();
  }

  liberarEventosAulaApuracao(seqs: number[]) {
    const url = `${this.baseUrl}LiberarEventosAulaApuracao`;
    return this.http.post(url, seqs).toPromise();
  }

  liberarEventosAulaCorrecao(seqs: number[]) {
    const url = `${this.baseUrl}LiberarEventosAulaCorrecao`;
    return this.http.post(url, seqs).toPromise();
  }

  alterarEventosAulasNaoExecutadaOuNaoApurada(seqs: number[], acao: string) {
    const url = `${this.baseUrl}AlterarEventosAulasNaoExecutadaOuNaoApurada`;
    const data = { seqsEventoAula: seqs, situacaoApuracaoFrequencia: acao };
    return this.http.post(url, data).toPromise();
  }

  buscarTokensSeguranca() {
    const url = `${this.baseUrl}BuscarTokensSeguranca`;
    return this.http.get<SmcTokensSeguranca[]>(url).toPromise();
  }
}
