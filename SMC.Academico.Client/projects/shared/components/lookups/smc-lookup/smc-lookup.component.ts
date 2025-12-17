import { Component, OnInit, Input, EventEmitter, Output, TemplateRef, OnChanges, SimpleChanges } from '@angular/core';
import { SmcKeyValueModel } from 'projects/shared/models/smc-key-value.model';
import { SmcLookupBuscaAvancadaComponent } from './smc-lookup-busca-avancada/smc-lookup-busca-avancada.component';
import { PoTableColumn } from '@po-ui/ng-components';
import { SMCPageSetting } from 'projects/shared/models/smc-page-setting.model';
import { SmcPagerDataModel } from 'projects/shared/models/smc-pager-data.model';
import { SmcLookupTurmaModel } from '../lk-tur-001-turma/smc-lookup-turma.model';
import { defaultKeyValueSelector, isNullOrEmpty } from '../../../utils/util';
import { FormGroup } from '@angular/forms';
import { SmcLookupCacheService } from './smc-lookup-cache.service';

@Component({
  selector: 'smc-lookup',
  templateUrl: './smc-lookup.component.html',
  standalone: false,
})
export class SmcLookupComponent implements OnInit, OnChanges {
  /**
   * @description
   *
   * Campo requerido
   *
   * @default `false`
   */
  @Input('s-required') required = false;
  /** Label do campo */
  @Input('s-label') label = 's-label';
  @Input('s-readonly') readonly = false;
  @Input('s-advanced-search-template') advancedSearchTemplate: TemplateRef<any>;
  @Input('s-advanced-search-form') form: FormGroup;
  // @Input('s-auto-complete-list') autoCompleteList: SmcKeyValueModel[] = [];
  @Input('s-selector') selector: (row: any) => SmcKeyValueModel = defaultKeyValueSelector;
  // @Input('s-advanced-list-data') advancedListData: SmcPagerDataModel<SmcLookupTurmaModel> = { itens: [], total: 0 };
  @Input('s-advanced-list-columns') advancedListColumns: PoTableColumn[] = [];
  @Input('s-auto-search') autoSearch = false;
  @Input('s-selected-item') selectedItem: SmcKeyValueModel = { key: '', value: '' };
  @Input('s-search-by-key') searchByKey = true;
  @Output('s-value-changed') onValueChanged = new EventEmitter<string>();
  // @Output('s-text-search') onTextSearch = new EventEmitter<string>();
  // @Output('s-advanced-search') onAdvancedSearch = new EventEmitter();
  @Output('s-advanced-search-clear') onAdvancedClear = new EventEmitter();
  // @Output('s-page-settings-changed') onPageSettingsChanged = new EventEmitter<SMCPageSetting>();

  autoCompleteList: SmcKeyValueModel[] = [];
  advancedListData: SmcPagerDataModel<SmcLookupTurmaModel> = { itens: [], total: 0 };
  onTextSearch = new EventEmitter<string>();
  onAdvancedSearch = new EventEmitter();
  onPageSettingsChanged = new EventEmitter<SMCPageSetting>();

  constructor(private lookupCacheService: SmcLookupCacheService) {}

  ngOnInit(): void {}

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['description']) {
      const description = changes['description'].currentValue as string;
      if (!isNullOrEmpty(description)) {
        this.selectedItem = { key: this.selectedItem.key, value: changes['description'].currentValue as string };
      }
    }
  }

  buscaAvancada(modal: SmcLookupBuscaAvancadaComponent) {
    modal.abriModal();
  }

  itemSelecionado(valor: SmcKeyValueModel) {
    this.selectedItem = valor;
    this.lookupCacheService.add(this.label, this.selectedItem?.key, this.selectedItem?.value);
    this.onValueChanged.emit(valor.key);
  }

  clearData() {
    this.advancedListData = { itens: [], total: 0 };
    this.onAdvancedClear.emit();
  }
}
