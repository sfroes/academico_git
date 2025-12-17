import { Component, OnInit, Input, Output, EventEmitter, ViewChild, ElementRef, AfterViewInit } from '@angular/core';

@Component({
  selector: 'smc-button',
  templateUrl: './smc-button.component.html',
  standalone: false,
})
export class SmcButtonComponent implements AfterViewInit {
  private _disabled?: boolean = false;
  private _label: string = 'smc-button';
  private _loading?: boolean = false;
  private _type: string = 'button';
  private _tootilp?: string = '';
  @ViewChild('button', { static: true }) buttonElement: ElementRef;

  @Input('s-button-class') buttonClass: string;
  /**
   * @optional
   *
   * @description Desabilita o smc-button e evita interação com o mesmo.
   *
   * @default false
   */
  @Input('s-disabled') set disabled(value: boolean) {
    this._disabled = value;
  }

  get disabled(): boolean {
    return this._disabled;
  }

  /**
   *@description Label do Botão
   *
   * Sets input
   */
  @Input('s-label') set label(value: string) {
    this._label = value;
  }

  get label() {
    return this._label;
  }

  /**
   * @description Tipo do botão
   *
   * @default button
   */
  @Input('s-type') set type(value: string) {
    this._type = value;
  }

  get type() {
    return this._type;
  }

  /**
   * @optional
   *
   * @description Tooltip no botao
   */
  @Input('s-tooltip') set tooltip(value: string) {
    this._tootilp = value;
  }

  get tooltip() {
    return this._tootilp;
  }

  /**
   * @optional
   *
   * @description
   *
   * Exibe um ícone de carregamento à esquerda do _label_ do botão.
   *
   * Quando esta propriedade estiver habilitada, desabilitará o botão.
   *
   * @default `false`
   */
  @Input('s-loading') set loading(value: boolean) {
    this._loading = value;
  }

  get loading() {
    return this._loading;
  }

  /**
   * @optional
   *
   * @description
   *
   * Aplica foco no elemento ao ser iniciado.
   * > Caso mais de um elemento seja configurado com essa propriedade,
   * o último elemento declarado com ela terá o foco.
   *
   * @default `false`
   */
  @Input('s-auto-focus') autoFocus: boolean = false;

  /** Ação que será executada quando o usuário clicar sobre o `smc-button`. */
  @Output('s-click') clickEvent = new EventEmitter();

  constructor() {}

  ngAfterViewInit() {
    if (this.autoFocus) {
      this.focus();
    }
  }

  getTooltip = () => this.tooltip ?? this.label;
  getClass = () => 'smc-btn-custom ' + (this.buttonClass ?? '');

  focus(): void {
    if (!this.disabled) {
      setTimeout(() => {
        this.buttonElement.nativeElement.focus();
      });
    }
  }
}
