import { Component, OnInit, Input, Output, EventEmitter, ViewChild } from '@angular/core';
import { PoTableColumn, PoTableComponent } from '@po-ui/ng-components';
import { Subject, Subscription } from 'rxjs';
import { SmcKeyValueModel } from '../../models/smc-key-value.model';
import { SMCPageSetting } from '../../models/smc-page-setting.model';
import { SmcPagerDataModel } from '../../models/smc-pager-data.model';

@Component({
  selector: 'smc-table',
  templateUrl: './smc-table.component.html',
  styles: [],
})
export class SmcTableComponent implements OnInit {
  currentPage = 1;
  @ViewChild(PoTableComponent, { static: true }) poTable: PoTableComponent;
  @Input('s-advancedListData') pagerList: SmcPagerDataModel<any> = { itens: [], total: 0 };
  @Input('s-advancedListColumns') coluns: PoTableColumn[] = [];
  @Input('s-selectable') selectable: boolean;
  @Input('s-single-select') singleSelect: boolean;
  @Input('s-selector') selector: (row: any) => SmcKeyValueModel = () => null;
  @Input('s-clear-page-settings') onClearPageSettings: EventEmitter<any>;
  @Output('s-selectedItem')
  selectedItem = new EventEmitter<SmcKeyValueModel>();
  @Output('s-selected') selected = new EventEmitter<any>();
  @Output('s-page-settings-changed')
  pageSettingsChanged = new EventEmitter<SMCPageSetting>();

  constructor() {}

  ngOnInit(): void {}

  getSelectedRows() {
    return this.poTable.getSelectedRows();
  }

  onSelect(line) {
    const item = this.selector(line);
    this.selectedItem.emit(item);
    this.selected.emit(line);
  }
}
