import {
  Component,
  OnInit,
  Input,
  Output,
  EventEmitter,
  forwardRef,
  ViewChild,
  Injector,
  OnDestroy,
} from '@angular/core';
import {
  AbstractControl,
  ControlValueAccessor,
  NgControl,
  NG_VALIDATORS,
  NG_VALUE_ACCESSOR,
  ValidationErrors,
  Validator,
} from '@angular/forms';
import { SmcKeyValueModel } from 'projects/shared/models/smc-key-value.model';
import { PoSelectComponent, PoSelectOption } from '@po-ui/ng-components';
import { isNullOrEmpty } from 'projects/shared/utils/util';
import { Subscription } from 'rxjs';

@Component({
  selector: 'smc-select',
  templateUrl: './smc-select.component.html',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi: true,
      useExisting: forwardRef(() => SmcSelectComponent),
    },
    {
      provide: NG_VALIDATORS,
      useExisting: forwardRef(() => SmcSelectComponent),
      multi: true,
    },
  ],
})
export class SmcSelectComponent implements OnInit, OnDestroy, ControlValueAccessor, Validator {
  private _options: PoSelectOption[] = [];
  private _value: string = undefined;
  private _subscriptions: Subscription[] = [];
  @Input('s-class') class = '';
  @Input('s-name') name = '';
  @Output('s-change') onChange = new EventEmitter<string | number>();
  @Input('s-disabled') disabled = false;
  @Input('s-label') label = 's-label';
  @Input('s-options') set options(data: SmcKeyValueModel[]) {
    this._options = [{ value: '', label: 'Selecionar...' }];
    data.forEach(f => this._options.push({ value: f.key, label: f.value }));
    this.autoSelectSingleItem();
  }
  @Input('s-required') required = false;
  @Input('s-readonly') readonly = false;
  @Input('s-auto-select-single-item') autoSelect = false;
  @Input('s-placeholder') placeholder = 'Selecionar...';
  @ViewChild(PoSelectComponent) select: PoSelectComponent;

  get poOptions() {
    return this._options;
  }

  get value() {
    return this._value;
  }
  set value(val: string) {
    this._value = val;
    if (!isNullOrEmpty(val)) {
      this.select?.element.nativeElement.classList.add('ng-dirty');
    }
    this.onChangeCb(val);
  }

  constructor(private injector: Injector) {}

  validate(control: AbstractControl): ValidationErrors {
    if (this.required && !this.disabled && isNullOrEmpty(control.value)) {
      return {
        required: {
          valid: false,
        },
      };
    }
  }

  onChangeCb: (_: any) => void = () => {};
  onTouchedCb: (_: any) => void = () => {};

  writeValue(obj: any): void {
    this._value = obj;
    if (isNullOrEmpty(obj)) {
      this.select?.element.nativeElement.classList.remove('ng-dirty');
    }
    this.onChange.next(obj);
  }
  registerOnChange(fn: any): void {
    this.onChangeCb = fn;
    this.autoSelectSingleItem();
  }

  registerOnTouched(fn: any): void {
    this.onTouchedCb = fn;
  }

  setDisabledState?(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }

  ngOnInit(): void {
    // Executa no próximo loop do JS, após todas validações da tela
    Promise.resolve().then(() => {
      const hostControl = this.injector.get(NgControl);
      this._subscriptions.push(
        hostControl.statusChanges.subscribe(status => {
          if (status === 'INVALID') {
            this.select?.element.nativeElement.classList.remove('ng-valid');
            this.select?.element.nativeElement.classList.add('ng-invalid');
          }
          if (status === 'VALID') {
            this.select?.element.nativeElement.classList.remove('ng-invalid');
            this.select?.element.nativeElement.classList.add('ng-valid');
          }
        })
      );
    });
  }

  ngOnDestroy(): void {
    this._subscriptions.forEach(f => f.unsubscribe());
  }

  onChangeSelectOption() {}

  private autoSelectSingleItem() {
    if (this.autoSelect && this._options.length === 2) {
      this.value = this._options[1].value as string;
      this.onChange.emit(this.value);
    }
  }
}
