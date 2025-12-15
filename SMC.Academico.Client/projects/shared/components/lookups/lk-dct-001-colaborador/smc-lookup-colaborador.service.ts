import { Injectable } from '@angular/core';
import { SmcLookupService } from '../smc-lookup/smc-lookup.service';
import { HttpClient } from '@angular/common/http';
import { SmcKeyValueModel } from '../../../models/smc-key-value.model';
import { SmcPagerDataModel } from '../../../models/smc-pager-data.model';
import { SmcLookupColaboradorModel } from './smc-lookup-colaborador.model';

@Injectable({
  providedIn: 'root',
})
export class SmcLookupColaboradorService implements SmcLookupService {
  seqTruma: string;
  constructor(private http: HttpClient) {}
  searchByKey(key: string): Promise<SmcKeyValueModel> {
    const filtro = { seq: key };
    return this.buscarColaborador(filtro).then(result => {
      if (result?.total === 1) {
        return { key: result.itens[0].seq, value: result.itens[0].nome };
      }
      return null;
    });
  }

  searchByText(key: string): Promise<SmcKeyValueModel[]> {
    return this.buscarSelectColaborador(key);
  }

  searchByFilter(filter: any): Promise<SmcPagerDataModel<any>> {
    return this.buscarColaborador(filter);
  }

  buscarSelectColaborador(filtro: string) {
    const url = '../LookupColaboradorRoute/BuscarSelectColaboradores';
    const data = {
      seqTruma: this.seqTruma,
      nome: filtro,
      pageSettings: { pageSize: 10 },
    };
    return this.http.post<SmcKeyValueModel[]>(url, data).toPromise();
  }

  buscarColaborador(filtro: any) {
    const url = '../LookupColaboradorRoute/BuscarColaboradores';
    if (!filtro.pageSettings) {
      filtro = { ...filtro, pageSettings: { pageSize: 10 } };
    }
    return this.http.post<SmcPagerDataModel<SmcLookupColaboradorModel>>(url, filtro).toPromise();
  }

  dataSourceEntidadeResposavel() {
    const url = '../LookupColaboradorRoute/BuscarDataSourceEntidadesResponsaveis';
    return this.http.get<SmcKeyValueModel[]>(url).toPromise();
  }

  dataSourceTipoVinculo() {
    const url = '../LookupColaboradorRoute/BuscarDataSourceTiposViculoColaborador';
    return this.http.get<SmcKeyValueModel[]>(url).toPromise();
  }

  dataSourceTipoAtividade() {
    const url = '../LookupColaboradorRoute/BuscarDataSourceTiposAtividadeColaborador';
    return this.http.get<SmcKeyValueModel[]>(url).toPromise();
  }

  dataSourceSituacaoColaboradorInstituicao() {
    const url = '../LookupColaboradorRoute/BuscarDataSourceSituacaoColaboradorInstituicao';
    return this.http.get<SmcKeyValueModel[]>(url).toPromise();
  }
}
