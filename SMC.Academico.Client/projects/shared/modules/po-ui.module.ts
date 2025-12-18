import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  PoFieldModule,
  PoModalModule,
  PoLoadingModule,
  PoDropdownModule,
  PoDialogModule,
  PoTabsModule,
  PoTableModule,
  PoTooltipModule,
} from '@po-ui/ng-components';

@NgModule({
  declarations: [],
  imports: [CommonModule, PoFieldModule, PoModalModule, PoLoadingModule, PoDropdownModule, PoDialogModule, PoTabsModule, PoTableModule, PoTooltipModule ],
  exports: [
    PoFieldModule,
    PoModalModule,
    PoLoadingModule,
    PoDropdownModule,
    PoDialogModule,
    PoTabsModule,
    PoTableModule,
    PoTooltipModule
  ],
})
export class PoUiModule {}
