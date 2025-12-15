import { Injectable } from '@angular/core';
import { isNullOrEmpty } from 'projects/shared/utils/util';

@Injectable({
  providedIn: 'root',
})
export class SmcLookupCacheService {
  private _data: { key: string; value: { key: string; value: string }[] }[] = [];

  add(lookup: string, key: string, value: string): void {
    if(isNullOrEmpty(key))
    {
      return;
    }
    let lk = this._data.find(f => f.key === lookup);
    if (!lk) {
      lk = { key: lookup, value: [] };
      this._data.push(lk);
    }
    if (!lk.value.find(f => f.key === key)) {
      lk.value.push({ key, value });
    }
  }

  get(lookup:string, key:string): string {
    let lk = this._data.find(f => f.key === lookup);
    if (!lk) {
      lk = { key: lookup, value: [] };
      this._data.push(lk);
    }
    return lk.value.find(f => f.key === key)?.value;
  }
}
