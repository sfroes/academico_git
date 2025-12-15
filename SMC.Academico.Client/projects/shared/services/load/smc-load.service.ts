import { Injectable } from "@angular/core";
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: "root",
})
export class SmcLoadService {
  private _loadingCount: number = 0;
  private _isLoading: BehaviorSubject<boolean> = new BehaviorSubject(false);

  constructor() {}

  get isLoading() {
    return this._isLoading.asObservable();
  }

  startLoading() {
    this._loadingCount++;
    this._isLoading.next(true);
  }

  endLoading() {
    if(this._loadingCount > 0) {
      this._loadingCount --;
    }
    this._isLoading.next(this._loadingCount > 0);
  }
}
