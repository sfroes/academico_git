import { ChangeDetectionStrategy, Component, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { PoRadioGroupOption } from '@po-ui/ng-components';
import { convertToBoolean } from '../../../utils/util';
import { SmcTreeViewItem } from '../smc-tree-view-item/smc-tree-view-item.interface';

@Component({
  selector: 'smc-tree-view-item-header',
  templateUrl: './smc-tree-view-item-header.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: false,
})
export class SmcTreeViewItemHeaderComponent {
  private _selectableSingleItem: boolean = false;

  @ViewChild('inputCheckbox') inputCheckbox;

  @Input('s-item') item: SmcTreeViewItem;

  @Input('s-selectable') selectable: boolean = false;

  @Input('s-selectable-single-item') set selectableSingleItem(value: boolean) {
    this._selectableSingleItem = convertToBoolean(value);
  }

  get selectableSingleItem() {
    return this._selectableSingleItem;
  }

  @Output('s-expanded') expanded = new EventEmitter<MouseEvent>();

  @Output('s-selected') selected = new EventEmitter<any>();

  get hasSubItems() {
    return !!(this.item.subItems && this.item.subItems.length);
  }

  get option(): PoRadioGroupOption[] {
    return [{
      label: this.item.label,
      value: 'true'
    }];
  }
}
