import {
  Component,
  ViewChild,
  Input,
  TemplateRef,
  Output,
  EventEmitter, OnChanges, SimpleChanges
} from '@angular/core';
import { PoModalAction, PoModalComponent, PoTableColumn } from '@po-ui/ng-components';
import { SmcKeyValueModel } from '../../../../models/smc-key-value.model';
import { SMCPageSetting } from 'projects/shared/models/smc-page-setting.model';
import { SmcPagerDataModel } from 'projects/shared/models/smc-pager-data.model';
import { SmcTableComponent } from 'projects/shared/components/smc-table/smc-table.component';
import { SmcNotificationService } from 'projects/shared/services/notification/smc-notification.service';
import { defaultKeyValueSelector } from 'projects/shared/utils/util';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'smc-lookup-busca-avancada',
  templateUrl: './smc-lookup-busca-avancada.component.html',
})
export class SmcLookupBuscaAvancadaComponent implements OnChanges {
  searched = false;
  @ViewChild(PoModalComponent, { static: true }) modalBuscaAvancada: PoModalComponent;
  @ViewChild(SmcTableComponent, { static: true }) smcTable: SmcTableComponent;
  @Input('s-template') template: TemplateRef<any>;
  @Input('s-selector') selector: (row: any) => SmcKeyValueModel = defaultKeyValueSelector;
  @Input('s-autoCompleteList') autoCompleteList: SmcKeyValueModel[] = [];
  @Input('s-advancedListData') advancedListData: SmcPagerDataModel<any>; // = { itens: [], total: 0 };
  @Input('s-advancedListColumns') advancedListColumns: PoTableColumn[] = [];
  @Input('s-auto-search') autoSearch = false;
  @Input('s-searchDisabled') searchDisabled = false;
  @Input('s-selected-item') selectedItem: SmcKeyValueModel;
  @Input('s-form') filterForm: FormGroup;
  @Output('s-search') search = new EventEmitter();
  @Output('s-clear') clear = new EventEmitter();
  @Output('s-selected') onSelected = new EventEmitter<SmcKeyValueModel>();
  @Output('s-page-settings-changed') pageSettingsChanged = new EventEmitter<SMCPageSetting>();

  constructor(private notificationService: SmcNotificationService) {}

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['advancedListData']) {
      const key = this.selectedItem?.key;
      this.advancedListData?.itens?.forEach(item => {
        if (this.selector(item)?.key === key) {
          item.$selected = true;
        }
      });
    }
  }

  onSearch() {
    this.searched = true;
    this.search.emit();
  }

  acaoFechar: PoModalAction = {
    action: () => {
      this.fechar();
    },
    label: 'Fechar',
    danger: true,
  };

  acaoSalvar: PoModalAction = {
    action: () => {
      this.salvar();
    },
    label: 'Selecionar',
    disabled: true,
  };

  fechar() {
    this.clear.emit();
    this.modalBuscaAvancada.close();
  }

  salvar() {
    const selectedItems = this.smcTable.getSelectedRows();
    if (selectedItems.length === 0) {
      this.notificationService.warning('Selecione ao menos uma opção');
      return;
    }
    this.onSelected.emit(this.selector(selectedItems[0]));
    this.fechar();
  }

  abriModal() {
    // Forçar redesenhar os parâmetros
    this.clear.emit();
    this.modalBuscaAvancada.open();
    if (this.autoSearch) {
      this.onSearch();
    }
  }

  changePageSettings(pageSettings: SMCPageSetting) {
    this.pageSettingsChanged.emit(pageSettings);
  }
}
