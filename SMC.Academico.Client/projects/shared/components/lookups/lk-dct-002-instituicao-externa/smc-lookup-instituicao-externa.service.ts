import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SmcKeyValueModel } from '../../../models/smc-key-value.model';
import { SmcPagerDataModel } from '../../../models/smc-pager-data.model';
import { SmcLookupInstituicaoExternaModel } from './smc-lookup-instituicao-externa.model';
import { PoSelectOption } from '@po-ui/ng-components';
import { SmcLookupService } from '../smc-lookup/smc-lookup.service';

@Injectable({
  providedIn: 'root',
})
export class SmcLookupInstituicaoExternaService implements SmcLookupService {
  constructor(private http: HttpClient) {}

  async searchByKey(key: string): Promise<SmcKeyValueModel> {
    const filter = { seq: key };
    const result = await this.searchByFilter(filter);
    if (result?.total === 1) {
      return { key: result.itens[0].seq, value: result.itens[0].nome };
    }
    return null;
  }

  searchByText(key: string): Promise<SmcKeyValueModel[]> {
    const url = '../LookupInstituicaoExternaRoute/BuscarSelectInstituicoesExternas';
    const data = { nome: key, pageSettings: { pageSize: 10 } };
    return this.http.post<SmcKeyValueModel[]>(url, data).toPromise();
  }

  searchByFilter(filter: any): Promise<SmcPagerDataModel<SmcLookupInstituicaoExternaModel>> {
    const url = '../LookupInstituicaoExternaRoute/BuscarInstituicoesExternas';
    if (!filter.pageSettings) {
      filter = { ...filter, pageSettings: { pageSize: 10 } };
    }
    return this.http.post<SmcPagerDataModel<SmcLookupInstituicaoExternaModel>>(url, filter).toPromise();
  }

  dataSourceCategoriasInstituicaoEnsino() {
    const url = '../LookupInstituicaoExternaRoute/BuscarDataSourceCategoriasInstituicaoEnsino';
    return this.http.get<SmcKeyValueModel[]>(url).toPromise();
  }

  dataSourcePaisesValidosCorreios() {
    const url = '../LookupInstituicaoExternaRoute/BuscarDataSourcePaisesValidosCorreios';
    return this.http.get<SmcKeyValueModel[]>(url).toPromise();
  }

  dataSourceTipoInstituicaoEnsino() {
    const url = '../LookupInstituicaoExternaRoute/BuscarDataSourceTipoInstituicaoEnsino';
    return this.http.get<SmcKeyValueModel[]>(url).toPromise();
  }
}
