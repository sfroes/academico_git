import { Injectable } from '@angular/core';

import { Subject } from 'rxjs';

import { SmcTreeViewItem } from '../smc-tree-view-item/smc-tree-view-item.interface';
import { SmcTreeViewBaseModel } from './../smc-tree-view-base.model';
import { isNullOrEmpty } from 'projects/shared/utils/util';

@Injectable({
  providedIn: 'root',
})
export class SmcTreeViewService {
  private expandedEvent = new Subject<SmcTreeViewItem>();
  private selectedEvent = new Subject<SmcTreeViewItem>();

  emitExpandedEvent(treeViewItem: SmcTreeViewItem) {
    return this.expandedEvent.next(treeViewItem);
  }

  emitSelectedEvent(treeViewItem: SmcTreeViewItem) {
    return this.selectedEvent.next(treeViewItem);
  }

  onExpand() {
    return this.expandedEvent.asObservable();
  }

  onSelect() {
    return this.selectedEvent.asObservable();
  }

  clone(tree: Array<SmcTreeViewItem>) {
    const result = new Array<SmcTreeViewItem>();
    tree?.forEach(item => {
      const { subItems, ...currentItem } = item;
      const clonedSubItems = this.clone(subItems);
      result.push({ ...currentItem, subItems: clonedSubItems });
    });
    return result;
  }

  /**
   * @description monta a arvoré baseado no modelo base vindo do back
   *
   * @param bases Dados do do back end
   *
   * @param expanded Arvoré virá inicializará aberta
   */
  mountItemsTree(bases: SmcTreeViewBaseModel[], expanded: boolean = false){
    const result: SmcTreeViewItem[] = []
    bases.forEach(base => {
      if(isNullOrEmpty(base.seqPai) && !base.itemFolha){
        let item = {
          label: base.descricao,
          value: base.seq,
          selectable: base.selecionavel,
          subItems: this.mountSubItemTree(bases, base.seq, expanded)
        } as SmcTreeViewItem;
        result.push(item);
      }
    });
    return result;
  }

  private mountSubItemTree(bases: SmcTreeViewBaseModel[], seqPai: string, expanded: boolean){
    const children = bases.filter(f => f.seqPai === seqPai).map(m => m.seq);
    const result: SmcTreeViewItem[] = []

    children.forEach(son => {
      const childrenFound = bases.find(f => f.seq === son);
      let subItem = {
        label: childrenFound.descricao,
        value: childrenFound.seq,
        selectable: childrenFound.selecionavel,
        expanded: expanded
      } as SmcTreeViewItem;

      if(!childrenFound.itemFolha){
        subItem.subItems = this.mountSubItemTree(bases, childrenFound.seq, expanded)
      }
      result.push(subItem);
    });
    return result;
  }
}
