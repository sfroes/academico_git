import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PoMultiselectOption, PoSelectOption } from '@po-ui/ng-components';
import { SmcKeyValueModel } from 'projects/shared/models/smc-key-value.model';
import { SmcPagerDataModel } from 'projects/shared/models/smc-pager-data.model';
import { SmcLookupCursoOfertaLocalidadeModel } from './smc-lookup-curso-oferta-localidade.model';
import { SmcLookupService } from '../smc-lookup/smc-lookup.service';

@Injectable({
  providedIn: 'root',
})
export class SmcLookupCursoOfertaLocalidadeService implements SmcLookupService {
  constructor(private http: HttpClient) {}

  async searchByKey(key: string): Promise<SmcKeyValueModel> {
    const filter = { seq: key };
    const result = await this.searchByFilter(filter);
    if (result?.total === 1) {
      return { key: result.itens[0].seq, value: result.itens[0].descricaoOfertaCurso };
    }
    return null;
  }

  searchByText(key: string): Promise<SmcKeyValueModel[]> {
    const url = '../LookupCursoOfertaLocalidadeRoute/BuscarCursoOfertasLocalidadesLookupSelect';
    const data = { descricao: key, pageSettings: { pageSize: 10 } };
    return this.http.post<SmcKeyValueModel[]>(url, data).toPromise();
  }

  searchByFilter(filter: any): Promise<SmcPagerDataModel<SmcLookupCursoOfertaLocalidadeModel>> {
    const url = '../LookupCursoOfertaLocalidadeRoute/BuscarCursoOfertasLocalidadesLookup';
    if (!filter.pageSettings) {
      filter = { ...filter, pageSettings: { pageSize: 10 } };
    }
    return this.http.post<SmcPagerDataModel<SmcLookupCursoOfertaLocalidadeModel>>(url, filter).toPromise();
  }

  buscarDataSourceEntidadesResponsaveis() {
    const url = '../LookupCursoOfertaLocalidadeRoute/BuscarDataSourceEntidadesResponsaveis';
    return this.http.get<PoMultiselectOption[]>(url).toPromise();
  }

  buscarDataSourceNivelEnsino() {
    const url = '../LookupCursoOfertaLocalidadeRoute/BuscarDataSourceNivelEnsino';
    return this.http.get<SmcKeyValueModel[]>(url).toPromise();
  }

  buscarDataSourceSituacao() {
    const url = '../LookupCursoOfertaLocalidadeRoute/BuscarDataSourceSituacao';
    return this.http.get<SmcKeyValueModel[]>(url).toPromise();
  }

  buscarDataSourceTiposFormacaoEspecifica(seqNivelEnsino: string) {
    const url = '../LookupCursoOfertaLocalidadeRoute/BuscarDataSourceTiposFormacaoEspecifica';
    return this.http.post<SmcKeyValueModel[]>(url, { seqNivelEnsino }).toPromise();
  }

  buscarDataSourceLocalidade(seqNivelEnsino: string) {
    const url = '../LookupCursoOfertaLocalidadeRoute/BuscarDataSourceLocalidade';
    return this.http.post<SmcKeyValueModel[]>(url, { seqNivelEnsino }).toPromise();
  }

  buscarDataSourceMoadalidde(seqNivelEnsino: string) {
    const url = '../LookupCursoOfertaLocalidadeRoute/BuscarDataSourceModalidade';
    return this.http.post<SmcKeyValueModel[]>(url, { seqNivelEnsino }).toPromise();
  }
}
