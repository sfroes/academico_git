import { Injectable } from '@angular/core';
import { SmcKeyValueModel } from './../../../../../../../shared/models/smc-key-value.model';
import { SituacaoAulaLoteListaModel } from './../models/situacao-aula-lote-lista.model';
import { Subject } from 'rxjs';
import { SmcTokensSeguranca } from 'projects/shared/models/smc-tokens-seguranca.moldes';

@Injectable({
  providedIn: 'root',
})
export class SituacaoAulaLoteDataService {
  $atualizarListaAulas = new Subject();
  $carregando = new Subject<boolean>();
  $filtra = new Subject();

  private _dataSourceSituacaoAula: SmcKeyValueModel[] = [
    { key: '1', value: 'Executada' },
    { key: '22', value: 'Não apurada - dentro do prazo' },
    { key: '2', value: 'Não apurada - fora do prazo' },
    { key: '3', value: 'Não executada' },
  ];

  get dataSourceSituacaoAula() {
    return this._dataSourceSituacaoAula;
  }

  private _listaAulas: SituacaoAulaLoteListaModel[];

  get listaAulas() {
    return this._listaAulas;
  }

  set listaAulas(value: SituacaoAulaLoteListaModel[]) {
    this._listaAulas = value;
  }

  private _tokensSeguranca: SmcTokensSeguranca[] = [];

  get tokensSeguranca() {
    return this._tokensSeguranca;
  }

  set tokensSeguranca(value: SmcTokensSeguranca[]) {
    this._tokensSeguranca = value;
  }

  private _situacaoEscolhida: string;

  get situacaoEscolhida() {
    return this._situacaoEscolhida;
  }

  set situacaoEscolhida(value: string) {
    this._situacaoEscolhida = value;
  }

  constructor() {}
}
