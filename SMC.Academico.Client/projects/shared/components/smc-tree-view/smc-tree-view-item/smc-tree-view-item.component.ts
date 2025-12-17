import { animate, state, style, transition, trigger } from '@angular/animations';
import { ChangeDetectionStrategy, Component, Input } from '@angular/core';

import { SmcTreeViewItem } from './smc-tree-view-item.interface';
import { SmcTreeViewService } from '../services/smc-tree-view.service';
import { convertToBoolean } from '../../../utils/util';

@Component({
  selector: 'smc-tree-view-item',
  templateUrl: './smc-tree-view-item.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: false,
  animations: [
    trigger('toggleBody', [
      state(
        'collapsed',
        style({
          'overflow-y': 'hidden',
          visibility: 'hidden',
          opacity: 0,
          height: '0',
        })
      ),
      transition('expanded => collapsed', [
        style({ height: '*' }),
        animate(100, style({ opacity: 0 })),
        animate(200, style({ height: 0 })),
      ]),
      transition('collapsed => expanded', [
        style({ height: '0' }),
        animate(100, style({ opacity: 1 })),
        animate(200, style({ height: '*' })),
      ]),
    ]),
  ],
})
export class SmcTreeViewItemComponent {
  private _selectableSingleItem: boolean = false;

  @Input('s-item') item: SmcTreeViewItem;

  @Input('s-selectable') selectable: boolean;

  @Input('s-select-sheet-only') selectSheetOnly: boolean;

  @Input('s-all-selectable-items') allSelectedItmes: boolean;

  @Input('s-selectable-single-item') set selectableSingleItem(value: boolean) {
    this._selectableSingleItem = convertToBoolean(value);
  }

  get selectableSingleItem() {
    return this._selectableSingleItem;
  }

  get hasSubItems() {
    return !!(this.item.subItems && this.item.subItems.length);
  }

  constructor(private treeViewService: SmcTreeViewService) {}

  onClick(event: MouseEvent) {
    event.preventDefault();
    event.stopPropagation();

    this.item.expanded = !this.item.expanded;

    this.treeViewService.emitExpandedEvent({ ...this.item });
  }

  onSelect(selectedItem: SmcTreeViewItem) {
    this.treeViewService.emitSelectedEvent({ ...selectedItem });
  }

  trackByFunction(index: number) {
    return index;
  }
}
