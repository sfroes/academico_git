import {
  Input,
  Directive,
  ViewChild,
  AfterViewInit,
  ChangeDetectorRef,
  OnInit,
  OnChanges,
  SimpleChanges,
} from '@angular/core';
import { ControlValueAccessor, FormGroup } from '@angular/forms';
import { convertToBoolean, isNaturalNumber, isNullOrEmpty } from '../../../utils/util';
import { SmcLookupService } from './smc-lookup.service';
import { SmcLookupComponent } from './smc-lookup.component';
import { SmcLoadService } from 'projects/shared/services/load/smc-load.service';
import { SMCPageSetting } from 'projects/shared/models/smc-page-setting.model';
import { SmcLookupCacheService } from '../smc-lookup/smc-lookup-cache.service';

@Directive()
export abstract class SmcLookupBaseComponent implements ControlValueAccessor, OnChanges, OnInit, AfterViewInit {
  private _filterForm: FormGroup;
  private _readonly = false;
  private _required = false;
  protected initialFormValue: any = {};

  @Input('s-readonly') set readonly(value: boolean) {
    this._readonly = value;
    if (this.lookup) {
      this.lookup.readonly = value;
    }
  }
  get readonly() {
    return this._readonly;
  }
  @Input('s-required') set required(value: boolean) {
    this._required = value;
    if (this.lookup) {
      this.lookup.required = value;
    }
  }
  get required() {
    return this._required;
  }
  @ViewChild(SmcLookupComponent) lookup: SmcLookupComponent;

  constructor(
    protected lookupService: SmcLookupService,
    protected loadService: SmcLoadService,
    protected changeDetectorRef: ChangeDetectorRef,
    private lookupCacheService: SmcLookupCacheService
  ) {}

  ngOnChanges(changes: SimpleChanges): void {
    // Como o onChange pode executar antes do onInit,
    // guarda o valor de todas propriedades para passar para o form no reset.
    // As propriedades que não existirem no form serão ignoradas no reset.
    for (const prop in changes) {
      this.initialFormValue[prop] = changes[prop].currentValue;
    }
    this.resetForm();
  }

  ngOnInit(): void {
    this.createForm();
    // Reseta o form na criação para configurar alguma propriedade que já esteja configurada no onChange
    this.resetForm();
  }

  ngAfterViewInit(): void {
    this.lookupInit();
  }

  private _innerValue: any;

  set value(v: any) {
    if (v !== this._innerValue) {
      this._innerValue = v;
      this.onChangeCb(v);
      if (this.lookup && this.lookup.selectedItem) {
        this.lookupCacheService.add(this.lookup.label, v, this.lookup.selectedItem.value);
      }
    }
    if (isNullOrEmpty(v)) {
      if (this.lookup) {
        this.lookup.advancedListData = { itens: [], total: 0 };
      }
      this.resetForm();
    }
  }

  get value() {
    return this._innerValue;
  }

  set filterForm(form: FormGroup) {
    this._filterForm = form;
    if (this.lookup) {
      this.lookup.form = form;
    }
  }

  get filterForm() {
    return this._filterForm;
  }

  /**
   * @description Método utilizado para criar o form armazenado no filterForm
   */
  protected abstract createForm();

  /**
   * @description Verifica se um valor está preenchido
   * @param value Valor a ser verificado
   * @returns true caso esteja preenchido
   */
  isReadOnly(value: any): boolean {
    return !isNullOrEmpty(value) || value || value == 0;
  }

  onChangeCb: (_: any) => void = () => {};
  onTouchedCb: (_: any) => void = () => {};

  writeValue(v: any): void {
    this.value = v;
    if (!isNullOrEmpty(v)) {
      this.searchByKey(v);
    } else {
      if (this.lookup) {
        this.lookup.selectedItem = { key: '', value: '' };
      }
    }
  }

  registerOnChange(fn: any): void {
    this.onChangeCb = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouchedCb = fn;
  }

  setDisabledState?(isDisabled: boolean): void {
    this.readonly = isDisabled;
  }

  async searchByKey(key: string) {
    if (this.lookup) {
      this.lookup.selectedItem = await this.lookupService.searchByKey(key);
      if (this.lookup.selectedItem) {
        this.onChangeCb(this.lookup.selectedItem.key);
        this.lookupCacheService.add(this.lookup.label, this.lookup.selectedItem.key, this.lookup.selectedItem.value);
      }
    }
  }

  private resetForm() {
    this.filterForm?.reset(this.initialFormValue);
  }

  private lookupInit() {
    this.lookup.form = this.filterForm;
    this.lookup.readonly = this.readonly;
    this.lookup.required = this.required;
    if (this.value) {
      this.searchByKey(this.value);
    }
    this.changeDetectorRef.detectChanges();
    this.lookup.onValueChanged.subscribe(value => {
      this.onChangeCb(value);
      if (this.lookup.selectedItem) {
        this.lookupCacheService.add(this.lookup.label, this.lookup.selectedItem.key, this.lookup.selectedItem.value);
      }
    });
    this.lookup.onTextSearch.subscribe(text => {
      if (convertToBoolean(this.lookup.searchByKey) && isNaturalNumber(text)) {
        this.lookupService.searchByKey(text).then(result => {
          if (result) {
            this.lookup.selectedItem = result;
            if (this.lookup.selectedItem) {
              this.onChangeCb(this.lookup.selectedItem.key);
              this.lookupCacheService.add(this.lookup.label, this.lookup.selectedItem.key, this.lookup.selectedItem.value);
            }
          } else {
            this.lookup.autoCompleteList = [];
          }
        });
      } else {
        this.lookupService.searchByText(text).then(result => (this.lookup.autoCompleteList = result));
      }
    });
    this.lookup.onAdvancedSearch.subscribe(() => this.advancedSearch(this.filterForm.value));
    this.lookup.onPageSettingsChanged.subscribe(pageSettings => this.pageSettingsChanged(pageSettings));
    this.lookup.onAdvancedClear.subscribe(() => this.resetForm());
  }

  private async advancedSearch(filter: any) {
    this.loadService.startLoading();
    this.lookup.advancedListData = await this.lookupService.searchByFilter(filter);
    this.loadService.endLoading();
  }

  private async pageSettingsChanged(pageSettings: SMCPageSetting) {
    const filter = { ...this.filterForm.value, ...pageSettings };
    this.advancedSearch(filter);
  }
}
