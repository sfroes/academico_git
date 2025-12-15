import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SmcKeyValueModel } from '../../../models/smc-key-value.model';
import { SmcPagerDataModel } from '../../../models/smc-pager-data.model';
import { SmcLookupCicloLetivoModel } from './smc-lookup-ciclo-letivo.model';
import { PoSelectOption } from '@po-ui/ng-components';
import { SmcLookupService } from '../smc-lookup/smc-lookup.service';

@Injectable({
  providedIn: 'root',
})
export class SmcLookupCicloLetivoService implements SmcLookupService {
  constructor(private http: HttpClient) {}

  async searchByKey(key: string): Promise<SmcKeyValueModel> {
    const filter = { seq: key };
    const result = await this.searchByFilter(filter);
    if (result?.total === 1) {
      return { key: result.itens[0].seq, value: result.itens[0].descricao };
    }
    return null;
  }

  searchByText(key: string): Promise<SmcKeyValueModel[]> {
    const url = '../LookupCilcoLetivoRoute/BuscarCiclosLetivosLookupSelect';
    const data = { descricao: key, pageSettings: { pageSize: 10 } };
    return this.http.post<SmcKeyValueModel[]>(url, data).toPromise();
  }

  searchByFilter(filter: any): Promise<SmcPagerDataModel<SmcLookupCicloLetivoModel>> {
    const url = '../LookupCilcoLetivoRoute/BuscarCiclosLetivosLookup';
    if (!filter.pageSettings) {
      filter = { ...filter, pageSettings: { pageSize: 10 } };
    }
    return this.http.post<SmcPagerDataModel<SmcLookupCicloLetivoModel>>(url, filter).toPromise();
  }

  dataSourceRegimeLetivo() {
    const url = '../LookupCilcoLetivoRoute/BuscarDataSourceRegimeLetivo';
    return this.http.get<PoSelectOption[]>(url).toPromise();
  }
}
