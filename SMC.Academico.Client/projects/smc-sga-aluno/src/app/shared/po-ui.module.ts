import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  PoFieldModule,
  PoModalModule,
  PoLoadingModule,
  PoDropdownModule,
  PoDialogModule
} from '@po-ui/ng-components';

@NgModule({
  declarations: [],
  imports: [CommonModule, PoFieldModule, PoModalModule, PoDropdownModule, PoDialogModule],
  exports: [PoFieldModule, PoModalModule, PoLoadingModule, PoDropdownModule, PoDialogModule],
})
export class PoUiModule {}
