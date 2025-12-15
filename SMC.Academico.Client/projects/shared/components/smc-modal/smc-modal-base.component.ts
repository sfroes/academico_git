import { Input, EventEmitter, Directive, Output } from '@angular/core';

import { convertToBoolean } from './../../utils/util';

/**
 * @description
 *
 * * O componente `smc-modal` foi baseado no `smc-modal` removendo algumas ações
 *
 * O componente `smc-modal` é utilizado para incluir conteúdos rápidos e informativos.
 *
 * No cabeçalho do componente é possível definir um título e como também permite ocultar o ícone de fechamento da modal.
 *
 * O smc-modal será de corpo liver ficando a cargo do desenvolvedor todas configuração do seu corpo e rodapé
 *
 * > É possível fechar a modal através da tecla *ESC*, quando a propriedade `s-hide-close` não estiver habilitada.
 */
@Directive()
export class SmcModalBaseComponent {
  private _hideClose?: boolean = false;
  private _size?: string = 'md';

  /** Título da modal. */
  @Input('s-title') title: string;

  /**
   * Define o tamanho da modal.
   *
   * Valores válidos:
   *  - `sm` (pequeno)
   *  - `md` (médio)
   *  - `lg` (grande)
   *  - `xl` (extra grande)
   *  - `auto` (automático)
   *
   * > Quando informado `auto` a modal calculará automaticamente seu tamanho baseado em seu conteúdo.
   * Caso não seja informado um valor, a modal terá o tamanho definido como `md`.
   *
   * > Todas as opções de tamanho possuem uma largura máxima de **768px**.
   */
  @Input('s-size') set size(value: string) {
    const sizes = ['sm', 'md', 'lg', 'xl', 'auto'];
    this._size = sizes.indexOf(value) > -1 ? value : 'md';
  }

  get size() {
    return this._size;
  }

  /**
   * Define o fechamento da modal ao clicar fora da mesma.
   * Informe o valor `true` para ativar o fechamento ao clicar fora da modal.
   */
  clickOut?: boolean = false;
  @Input('s-click-out') set setClickOut(value: boolean | string) {
    this.clickOut = value === '' ? false : convertToBoolean(value);
  }

  /**
   * @optional
   *
   * @description
   *
   * Oculta o ícone de fechar do cabeçalho da modal.
   *
   * > Caso a propriedade estiver habilitada, não será possível fechar a modal através da tecla *ESC*.
   *
   * @default `false`
   */
  @Input('s-hide-close') set hideClose(value: boolean) {
    this._hideClose = convertToBoolean(value);
  }

  get hideClose() {
    return this._hideClose;
  }

  // Controla se a modal fica oculto ou visível, por padrão é oculto
  isHidden = true;

  // Event emmiter para quando a modal é fechada pelo 'X'.
  @Output('s-on-close') onXClosed = new EventEmitter<boolean>();

  /** Função para fechar a modal. */
  close(xClosed = false): void {
    this.isHidden = true;
    if (xClosed) {
      this.onXClosed.emit(xClosed);
    }
  }

  /** Função para abrir a modal. */
  open(): void {
    this.isHidden = false;
  }
}
