import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PoMultiselectOption } from '@po-ui/ng-components';
import { SmcKeyValueModel } from 'projects/shared/models/smc-key-value.model';
import { SmcPagerDataModel } from 'projects/shared/models/smc-pager-data.model';
import { SmcLookupCursoOfertaModel } from './smc-lookup-curso-oferta.model';
import { SmcLookupService } from '../smc-lookup/smc-lookup.service';

@Injectable({
  providedIn: 'root',
})
export class SmcLookupCursoOfertaService implements SmcLookupService {
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
    const url = '../LookupCursoOfertaRoute/BuscarCursoOfertasLookupSelect';
    const data = { descricao: key, pageSettings: { pageSize: 10 } };
    return this.http.post<SmcKeyValueModel[]>(url, data).toPromise();
  }

  searchByFilter(filter: any): Promise<SmcPagerDataModel<SmcLookupCursoOfertaModel>> {
    const url = '../LookupCursoOfertaRoute/BuscarCursoOfertasLookup';
    if (!filter.pageSettings) {
      filter = { ...filter, pageSettings: { pageSize: 10 } };
    }
    return this.http.post<SmcPagerDataModel<SmcLookupCursoOfertaModel>>(url, filter).toPromise();
  }

  buscarDataSourceEntidadesResponsaveis() {
    const url = '../LookupCursoOfertaRoute/BuscarDataSourceEntidadesResponsaveis';
    return this.http.get<PoMultiselectOption[]>(url).toPromise();
  }

  buscarDataSourceNivelEnsino() {
    const url = '../LookupCursoOfertaRoute/BuscarDataSourceNivelEnsino';
    return this.http.get<PoMultiselectOption[]>(url).toPromise();
  }

  buscarDataSourceSituacao() {
    const url = '../LookupCursoOfertaRoute/BuscarDataSourceSituacao';
    return this.http.get<SmcKeyValueModel[]>(url).toPromise();
  }

  buscarDataSourceTiposFormacaoEspecifica(seqsNivelEnsino: string[]) {
    const url = '../LookupCursoOfertaRoute/BuscarDataSourceTiposFormacaoEspecifica';
    return this.http.post<SmcKeyValueModel[]>(url, seqsNivelEnsino).toPromise();
  }

  // buscarDataSourceGrauAcademico(seqsNivelEnsino: string[]) {
  //   const url = '../LookupCursoOfertaRoute/BuscarDataSourceGrauAcademico';
  //   return this.http
  //     .post<SmcKeyValueModel[]>(url, { seqsNivelEnsino })
  //     .toPromise();
  // }
}
