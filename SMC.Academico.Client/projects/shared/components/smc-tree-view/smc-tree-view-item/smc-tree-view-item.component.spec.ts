import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';

import { PoFieldModule } from '@po-ui/ng-components';

import { SmcTreeViewItemComponent } from './smc-tree-view-item.component';
import { SmcTreeViewItemHeaderComponent } from '../smc-tree-view-item-header/smc-tree-view-item-header.component';
import { SmcTreeViewService } from '../services/smc-tree-view.service';

describe('SmcTreeviewItemComponent:', () => {
  let component: SmcTreeViewItemComponent;
  let fixture: ComponentFixture<SmcTreeViewItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [BrowserAnimationsModule, FormsModule, PoFieldModule],
      declarations: [SmcTreeViewItemComponent, SmcTreeViewItemHeaderComponent],
      providers: [SmcTreeViewService]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SmcTreeViewItemComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
    expect(component instanceof SmcTreeViewItemComponent).toBeTruthy();
  });

  describe('Properties:', () => {
    it('hasSubItems: should return true if has subItems', () => {
      component.item = {
        label: 'Nivel 0',
        value: '220',
        subItems: [{ label: 'Nivel 01', value: 11 }]
      };

      expect(component.hasSubItems).toBe(true);
    });

    it('hasSubItems: should return false if subItems is undefined', () => {
      component.item = {
        label: 'Nivel 0',
        value: '220',
        subItems: undefined
      };

      expect(component.hasSubItems).toBe(false);
    });
  });

  describe('Methods:', () => {
    it('onClick: should call event.preventDefault, event.stopPropagation and treeViewService.emitExpandedEvent with item', () => {
      component.item = { label: 'Label 01', value: 12 };

      const fakeEvent = {
        preventDefault: () => {},
        stopPropagation: () => {}
      };

      const spyPreventDefault = spyOn(fakeEvent, 'preventDefault');
      const spyStopPropagation = spyOn(fakeEvent, 'stopPropagation');
      const spyEmitEvent = spyOn(component['treeViewService'], 'emitExpandedEvent');

      component.onClick(<any>fakeEvent);

      expect(component.item.expanded).toBe(true);
      expect(spyPreventDefault).toHaveBeenCalled();
      expect(spyStopPropagation).toHaveBeenCalled();
      expect(spyEmitEvent).toHaveBeenCalledWith(component.item);
    });

    it('onSelect: should call treeViewService.emitSelectedEvent with item', () => {
      component.item = { label: 'Label 01', value: 12 };

      const spyEmitEvent = spyOn(component['treeViewService'], 'emitSelectedEvent');

      component.onSelect(component.item);

      expect(spyEmitEvent).toHaveBeenCalledWith(component.item);
    });
  });

  describe('Templates:', () => {
    it('should find .po-tree-view-item-group if has subItems', () => {
      component.item = {
        label: 'Nivel 01',
        subItems: [{ label: 'Nivel 02', value: 12 }],
        value: '110'
      };

      fixture.detectChanges();

      const treeViewItemGroup = fixture.debugElement.nativeElement.querySelector('.po-tree-view-item-group');
      expect(treeViewItemGroup).toBeTruthy();
    });

    it('shouldn`t find .po-tree-view-item-group if hasn`t subItems', () => {
      component.item = {
        label: 'Nivel 01',
        value: '1',
        subItems: undefined
      };

      fixture.detectChanges();

      const treeViewItemGroup = fixture.debugElement.nativeElement.querySelector('.po-tree-view-item-group');
      expect(treeViewItemGroup).toBe(null);
    });

    it('trackByFunction: should return index param', () => {
      expect(component.trackByFunction(1)).toBe(1);
    });
  });
});
