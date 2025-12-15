import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SmcPagerComponent } from './smc-pager.component';
import { PoUiModule } from 'projects/shared/modules/po-ui.module';
import { FormsModule } from '@angular/forms';
import { SmcButtonModule } from '../smc-button/smc-button.module';

@NgModule({
  declarations: [SmcPagerComponent],
  imports: [CommonModule, PoUiModule, FormsModule, SmcButtonModule],
  exports: [SmcPagerComponent],
})
export class SmcPagerModule {}
