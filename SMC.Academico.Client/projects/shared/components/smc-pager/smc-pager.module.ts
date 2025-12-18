import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SmcPagerComponent } from './smc-pager.component';
import { PoFieldModule } from '@po-ui/ng-components';
import { FormsModule } from '@angular/forms';
import { SmcButtonModule } from '../smc-button/smc-button.module';

@NgModule({
  declarations: [SmcPagerComponent],
  imports: [CommonModule, PoFieldModule, FormsModule, SmcButtonModule],
  exports: [SmcPagerComponent, PoFieldModule, FormsModule],
})
export class SmcPagerModule {}
