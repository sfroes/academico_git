import { SmcKeyValueModel } from 'projects/shared/models/smc-key-value.model';
import { SmcPagerDataModel } from 'projects/shared/models/smc-pager-data.model';

export interface SmcLookupService {
  searchByKey(key: string): Promise<SmcKeyValueModel>;
  searchByText(value: string): Promise<SmcKeyValueModel[]>;
  searchByFilter(filter: any): Promise<SmcPagerDataModel<any>>;
}
