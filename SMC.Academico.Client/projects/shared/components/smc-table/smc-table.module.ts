import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SmcTableComponent } from './smc-table.component';
import { PoTableModule } from '@po-ui/ng-components';
import { SmcPagerModule } from '../smc-pager/smc-pager.module';

@NgModule({
  declarations: [SmcTableComponent],
  imports: [
    CommonModule,
    PoTableModule,
    SmcPagerModule
  ],
  exports: [SmcTableComponent],
})
export class SmcTableModule {}
