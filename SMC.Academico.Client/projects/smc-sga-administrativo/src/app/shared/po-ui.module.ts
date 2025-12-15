import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  PoFieldModule,
  PoModalModule,
  PoLoadingModule,
  PoDropdownModule,
  PoDialogModule,
  PoTabsModule,
} from '@po-ui/ng-components';

@NgModule({
  declarations: [],
  imports: [CommonModule, PoFieldModule, PoModalModule, PoDropdownModule, PoDialogModule, PoTabsModule],
  exports: [PoFieldModule, PoModalModule, PoLoadingModule, PoDropdownModule, PoDialogModule, PoTabsModule],
})
export class PoUiModule {}
