import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';

import { SmcTreeViewBaseComponent } from './smc-tree-view-base.component';
import { SmcTreeViewItem } from './smc-tree-view-item/smc-tree-view-item.interface';
import { SmcTreeViewService } from './services/smc-tree-view.service';

/**
 * @docsExtends SmcTreeViewBaseComponent
 *
 * @example
 *
 *<smc-tree-view
 *  [s-items]="[{ label: 'Gerenciador de usuários', value: 1, subItems: [{ label: 'Adicionar', value: 1.1 }] }]">
 *</smc-tree-view>
 */
@Component({
  selector: 'smc-tree-view',
  templateUrl: './smc-tree-view.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [SmcTreeViewService],
})
export class SmcTreeViewComponent extends SmcTreeViewBaseComponent implements OnInit {
  get hasItems() {
    return !!(this.items && this.items.length);
  }

  constructor(private treeViewService: SmcTreeViewService) {
    super();
  }

  ngOnInit() {
    this.treeViewService.onExpand().subscribe((treeViewItem: SmcTreeViewItem) => {
      this.emitExpanded(treeViewItem);
    });

    this.treeViewService.onSelect().subscribe((treeViewItem: SmcTreeViewItem) => {
      this.emitSelected(treeViewItem);
    });
  }

  trackByFunction(index: number) {
    return index;
  }
}
