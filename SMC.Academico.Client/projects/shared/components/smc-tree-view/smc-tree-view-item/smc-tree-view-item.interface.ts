/**
 * @usedBy SmcTreeViewComponent
 *
 * @description
 *
 * Interface para definição dos itens do componente `smc-tree-view`.
 */
export interface SmcTreeViewItem {
  /** Desabilita a selecão do item. */
  // disabled?: boolean;

  /** Texto de exibição do item. */
  label: string;

  /** Valor do item que poderá ser utilizado como referência para sua identificação. */
  value: string | number;

  /** Expande o item. */
  expanded?: boolean;

  /**
   * Marca o item como selecionado.
   *
   * > Caso o item que conter `subItems` for selecionado, os seus itens filhos serão também selecionados.
   */
  selected?: boolean | null;

  /** Pode ser combinado a regra de item folha para difinir se o item é selecionável */
  selectable?: boolean | null;

  /** Lista de itens do próximo nível, e assim consecutivamente até que se atinja o quarto nível. */
  subItems?: Array<SmcTreeViewItem>;

  /** Preenchido com o item superior do item atual */
  parent?: SmcTreeViewItem | null;

  /** Casse que pode ser inserida no item atual */
  cssClass?: string | null;
}
