import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SmcKeyValueModel } from 'projects/shared/models/smc-key-value.model';
import { SmcLookupTurmaModel } from './smc-lookup-turma.model';
import { SmcPagerDataModel } from 'projects/shared/models/smc-pager-data.model';
import { PoMultiselectOption } from '@po-ui/ng-components';
import { SmcLookupService } from '../smc-lookup/smc-lookup.service';

@Injectable({
  providedIn: 'root',
})
export class SmcLookupTurmaService implements SmcLookupService {
  /**
   * @description Campo dependency
   */
  seqCicloLetivoInicio: string;
  constructor(private http: HttpClient) {}

  searchByKey(key: string): Promise<SmcKeyValueModel> {
    const filtro = { seq: key };
    return this.buscarTurmas(filtro).then(result => {
      if (result?.total === 1) {
        return { key: result.itens[0].seq, value: result.itens[0].descricaoConfiguracaoComponente };
      }
      return null;
    });
  }

  searchByText(key: string): Promise<SmcKeyValueModel[]> {
    return this.buscarSelectTurma(key);
  }

  searchByFilter(filter: any): Promise<SmcPagerDataModel<any>> {
    return this.buscarTurmas(filter);
  }

  buscarSelectTurma(filtro: string) {
    const url = '../LookupTurmaRoute/BuscarSelectTurmas';
    const data = {
      seqCicloLetivoInicio: this.seqCicloLetivoInicio,
      descricaoConfiguracao: filtro,
      pageSettings: { pageSize: 10 },
    };
    return this.http.post<SmcKeyValueModel[]>(url, data).toPromise();
  }

  buscarTurmas(filtro: any) {
    const url = '../LookupTurmaRoute/BuscarTurmas';
    if (!filtro.pageSettings) {
      filtro = { ...filtro, pageSettings: { pageSize: 10 } };
    }
    return this.http.post<SmcPagerDataModel<SmcLookupTurmaModel>>(url, filtro).toPromise();
  }

  buscarDataSourceEntidadesResponsaveis() {
    const url = '../LookupTurmaRoute/BuscarDataSourceEntidadesResponsaveis';
    return this.http.get<PoMultiselectOption[]>(url).toPromise();
  }
}
