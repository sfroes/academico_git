import { Component, OnInit, Output, EventEmitter, Input, OnDestroy } from '@angular/core';
import { PoSelectOption } from '@po-ui/ng-components';
import { Subscription } from 'rxjs';
import { SMCPageSetting } from '../../models/smc-page-setting.model';

@Component({
  selector: 'smc-pager',
  templateUrl: './smc-pager.component.html',
})
export class SmcPagerComponent implements OnInit, OnDestroy {
  pages: PoSelectOption[];
  pageSize = 10;
  private _currentPage = 1;
  private _onResetSubscription: Subscription;
  @Input('s-total-itens') totalItens = 0;
  @Input('s-reset') onReset: EventEmitter<any>;
  @Output('s-page-settings-changed') pageSettingsChanged = new EventEmitter<SMCPageSetting>();

  get currentPage() {
    return this._currentPage;
  }

  set currentPage(val: number) {
    this._currentPage = val;
    this.pageSettingsChanged.emit({ pageIndex: val, pageSize: this.pageSize, total: this.totalItens });
  }

  constructor() {}

  ngOnInit(): void {
    this.pages = [
      { value: 5, label: '5' },
      { value: 10, label: '10' },
      { value: 15, label: '15' },
      { value: 20, label: '20' },
      { value: 50, label: '50' },
    ];
    if(this.onReset){
      this._onResetSubscription = this.onReset.subscribe(_ => {
        this._currentPage = 1;
        this.pageSize = 10;
      });
    }
  }

  ngOnDestroy(): void {
    this._onResetSubscription?.unsubscribe();
  }

  changePageSize(page: number) {
    this.currentPage = 1;
  }

  getPages() {
    return Math.ceil(this.totalItens / this.pageSize);
  }

  getPageStatus() {
    const index = this.currentPage - 1;
    let firstItem = index * this.pageSize + 1;
    firstItem = this.totalItens < this.pageSize ? 1 : firstItem;
    let lastItem = firstItem - 1 + this.pageSize;
    lastItem = lastItem < this.totalItens ? lastItem : this.totalItens;
    return `Visualizando ${firstItem} a ${lastItem} de ${this.totalItens}`;
  }

  nextPage() {
    if (this.currentPage < this.getPages()) {
      this.currentPage++;
    }
  }

  previousPage() {
    if (this.currentPage > 1) {
      this.currentPage--;
    }
  }

  firstPage() {
    this.currentPage = 1;
  }

  lastPage() {
    this.currentPage = this.getPages();
  }
}
