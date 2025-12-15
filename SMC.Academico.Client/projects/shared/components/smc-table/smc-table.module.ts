import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SmcTableComponent } from './smc-table.component';
import { PoUiModule } from '../../modules/po-ui.module';
import { SmcPagerModule } from '../smc-pager/smc-pager.module';

@NgModule({
  declarations: [SmcTableComponent],
  imports: [CommonModule, PoUiModule, SmcPagerModule],
  exports: [SmcTableComponent],
})
export class SmcTableModule {}
