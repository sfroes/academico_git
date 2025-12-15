import { TestBed } from '@angular/core/testing';

import { Observable } from 'rxjs';

import { SmcTreeViewItem } from '../smc-tree-view-item/smc-tree-view-item.interface';
import { SmcTreeViewService } from './smc-tree-view.service';

describe('SmcTreeViewService:', () => {
  let treeViewService: SmcTreeViewService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SmcTreeViewService]
    });

    treeViewService = TestBed.inject(SmcTreeViewService);
  });

  it('emitExpandedEvent: should call expandedEvent.next with treeViewItem', () => {
    const treeViewItem: SmcTreeViewItem = {
      label: 'Nível 01',
      value: 1
    };

    const spyEmitExpandedEvent = spyOn(treeViewService['expandedEvent'], 'next');

    treeViewService.emitExpandedEvent(treeViewItem);

    expect(spyEmitExpandedEvent).toHaveBeenCalledWith(treeViewItem);
  });

  it('onExpand: should return an instanceof Observable', () => {
    const onExpand = treeViewService.onExpand();

    expect(onExpand instanceof Observable).toBeTruthy();
  });

  it('emitSelectedEvent: should call selectedEvent.next with treeViewItem', () => {
    const treeViewItem: SmcTreeViewItem = {
      label: 'Nível 01',
      value: 1
    };

    const spyEmitSelectedEvent = spyOn(treeViewService['selectedEvent'], 'next');

    treeViewService.emitSelectedEvent(treeViewItem);

    expect(spyEmitSelectedEvent).toHaveBeenCalledWith(treeViewItem);
  });

  it('onSelect: should return an instanceof Observable', () => {
    const onSelect = treeViewService.onSelect();

    expect(onSelect instanceof Observable).toBeTruthy();
  });
});
