import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SmcSelectComponent } from './smc-select.component';
import { PoUiModule } from '../../modules/po-ui.module';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [SmcSelectComponent],
  imports: [CommonModule, PoUiModule, FormsModule],
  exports: [SmcSelectComponent],
})
export class SmcSelectModule {}
