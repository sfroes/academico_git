import { EventEmitter, Input, Output, Directive } from '@angular/core';

import { convertToBoolean } from '../../utils/util';

import { SmcTreeViewItem } from './smc-tree-view-item/smc-tree-view-item.interface';

const smcTreeViewMaxLevel = 10;

/**
 * @description
 *
 * O componente fornece um modelo de visualização em árvore, possibilitando a visualização das informações de maneira
 * hierárquica, desta forma sendo possível utilizar até 10 níveis.
 *
 * Nele é possível navegar entre os itens através da tecla *tab*, permitindo expandir ou colapsar o item em foco
 * por meio das teclas *enter* e *space*.
 *
 * Além da navegação, o componente possibilita também a seleção dos itens do primeiro ao último nível, tanto de forma parcial como completa.
 *
 * O componente também possui eventos disparados ao marcar/desmarcar e expandir/colapsar os itens.
 */
@Directive()
export class SmcTreeViewBaseComponent {
  private _items: Array<SmcTreeViewItem> = [];
  private _selectable: boolean = false;
  private _selectableSingleItem: boolean = false;
  private _selectSheetOnly: boolean = false;
  private _allSelectableItems: boolean = false;

  /**
   * Lista de itens do tipo `PoTreeViewItem` que será renderizada pelo componente.
   */
  @Input('s-items') set items(value: Array<SmcTreeViewItem>) {
    this._items = Array.isArray(value) ? this.getItemsByMaxLevel(value) : [];
  }

  get items() {
    return this._items;
  }

  /**
   * @optional
   *
   * @description
   *
   * Habilita uma caixa de seleção para selecionar e/ou desmarcar um item da lista.
   *
   * @default false
   */
  @Input('s-selectable') set selectable(value: boolean) {
    this._selectable = convertToBoolean(value);
  }

  get selectable() {
    return this._selectable;
  }

     /**
   * @optional
   *
   * @description
   *
   * Habilita a seleção somente do item folha na árvore.
   *
   * @default false
   */
  @Input('s-select-sheet-only') set selectSheetOnly(value: boolean) {
    this._selectSheetOnly = convertToBoolean(value);
  }

  get selectSheetOnly() {
    return this._selectSheetOnly;
  }

   /**
   * @optional
   *
   * @description
   *
   * Habilita a seleção em todos os itens sejam selecionávies.
   *
   * @default false
   */
  @Input('s-all-selectable-items') set allSelectedItmes(value: boolean) {
    this._allSelectableItems = convertToBoolean(value);
  }

  get allSelectedItmes() {
    return this._allSelectableItems;
  }

  /**
   * @optional
   *
   * @description
   *
   * Habilita uma caixa de seleção para selecionar e/ou desmarcar um item da lista.
   *
   * @default false
   */
  @Input('s-selectable-single-item') set selectableSingleItem(value: boolean) {
    this._selectableSingleItem = convertToBoolean(value);
  }

  get selectableSingleItem() {
    return this._selectableSingleItem;
  }

  /**
   * @optional
   *
   * @description
   *
   * Ação que será disparada ao colapsar um item.
   *
   * > Como parâmetro o componente envia o item colapsado.
   */
  @Output('s-collapsed') collapsed = new EventEmitter<SmcTreeViewItem>();

  /**
   * @optional
   *
   * @description
   *
   * Ação que será disparada ao expandir um item.
   *
   * > Como parâmetro o componente envia o item expandido.
   */
  @Output('s-expanded') expanded = new EventEmitter<SmcTreeViewItem>();

  /**
   * @optional
   *
   * @description
   *
   * Ação que será disparada ao selecionar um item.
   *
   * > Como parâmetro o componente envia o item selecionado.
   */
  @Output('s-selected') selected = new EventEmitter<SmcTreeViewItem>();

  /**
   * @optional
   *
   * @description
   *
   * Ação que será disparada ao desfazer a seleção de um item.
   *
   * > Como parâmetro o componente envia o item que foi desmarcado.
   */
  @Output('s-unselected') unselected = new EventEmitter<SmcTreeViewItem>();

  protected emitExpanded(treeViewItem: SmcTreeViewItem) {
    const event = treeViewItem.expanded ? 'expanded' : 'collapsed';

    this[event].emit({ ...treeViewItem });
  }

  protected emitSelected(treeViewItem: SmcTreeViewItem) {
    const event = treeViewItem.selected ? 'selected' : 'unselected';

    this.updateItemsOnSelect(treeViewItem);

    this[event].emit({ ...treeViewItem });
  }

  private addChildItemInParent(childItem: SmcTreeViewItem, parentItem: SmcTreeViewItem) {
    if (!parentItem.subItems) {
      parentItem.subItems = [];
    }

    parentItem.subItems.push(childItem);
    childItem.parent = parentItem;
  }

  // caso houver parentItem:
  //  - expande o parentItem caso o filho estiver expandido;
  //  - adiciona o childItem no parentItem;
  //  - marca o parentItem caso conter subItems marcodos ou nulos;
  // Se não conter parentItem, adiciona o childItem no items.
  private addItem(items: Array<SmcTreeViewItem>, childItem: SmcTreeViewItem, parentItem?: SmcTreeViewItem) {
    if (parentItem) {
      this.expandParentItem(childItem, parentItem);
      this.addChildItemInParent(childItem, parentItem);
      if (!this.selectableSingleItem) {
        this.selectItemBySubItems(parentItem);
      }

      items.push(parentItem);
    } else {
      items.push(childItem);
    }
  }

  private selectAllItems(items: Array<SmcTreeViewItem>, isSelected: boolean) {
    items.forEach(item => {
      if (item.subItems) {
        this.selectAllItems(item.subItems, isSelected);
      }

      item.selected = isSelected;
    });
  }

  private selectItemBySubItems(item: SmcTreeViewItem) {
    item.selected = this.everyItemSelected(item.subItems);
  }

  // retornará:
  //  - true: se todos os items estiverem marcados;
  //  - null: se no minimo um item esteja marcado ou nullo (indeterminate)
  //  - false: caso não corresponda em nenhuma das opções acima, no caso, nenhum marcado ou nulo;
  private everyItemSelected(items: Array<SmcTreeViewItem> = []): boolean | null {
    const itemsLength = items.length;

    const lengthCheckedItems = items.filter(item => item.selected).length;

    if (itemsLength && itemsLength === lengthCheckedItems) {
      return true;
    }

    const hasIndeterminateItems = items.filter(item => item.selected || item.selected === null).length;

    if (hasIndeterminateItems) {
      return null;
    }

    return false;
  }

  // expande o item pai caso o filho estiver expandido.
  private expandParentItem(childItem: SmcTreeViewItem, parentItem: SmcTreeViewItem) {
    if (childItem.expanded) {
      parentItem.expanded = true;
    }
  }

  private getItemsByMaxLevel(
    items: Array<SmcTreeViewItem> = [],
    level: number = 0,
    parentItem?: SmcTreeViewItem,
    newItems = []
  ) {
    items.forEach(item => {
      const { subItems, ...currentItem } = item;

      if (level === smcTreeViewMaxLevel) {
        return;
      }

      if (Array.isArray(subItems)) {
        // caso um item pai iniciar selecionado, deve selecionar os filhos, caso não seja seleção simples.
        if (currentItem.selected && !this.selectSingleItem) {
          this.selectAllItems(subItems, currentItem.selected);
        }

        this.getItemsByMaxLevel(subItems, ++level, currentItem);
        --level;
      }

      this.addItem(newItems, currentItem, parentItem);
    });

    return newItems;
  }

  private getItemsWithParentSelected(items: Array<SmcTreeViewItem> = [], parentItem?: SmcTreeViewItem, newItems = []) {
    items.forEach(item => {
      const { subItems, ...currentItem } = item;

      if (Array.isArray(subItems)) {
        this.getItemsWithParentSelected(subItems, currentItem);
      }

      this.addItem(newItems, currentItem, parentItem);
    });

    return newItems;
  }

  private updateItemsOnSelect(selectedItem: SmcTreeViewItem) {
    if (selectedItem.subItems) {
      this.selectAllItems(selectedItem.subItems, selectedItem.selected);
    }

    if (!this.selectableSingleItem) {
      this._items = this.getItemsWithParentSelected(this.items);
    } else if (selectedItem.selected) {
      this._items = this.selectSingleItem(selectedItem, this.items);
    }
  }

  private selectSingleItem(
    selectedItem: SmcTreeViewItem,
    items: Array<SmcTreeViewItem>,
    parentItem?: SmcTreeViewItem,
    updatedItems: Array<SmcTreeViewItem> = []
  ) {
    items.forEach(item => {
      const { subItems, ...currentItem } = item;

      currentItem.selected = currentItem.value === selectedItem.value;

      if (Array.isArray(subItems)) {
        this.selectSingleItem(selectedItem, subItems, currentItem);
      }

      this.addItem(updatedItems, currentItem, parentItem);
    });

    return updatedItems;
  }
}
