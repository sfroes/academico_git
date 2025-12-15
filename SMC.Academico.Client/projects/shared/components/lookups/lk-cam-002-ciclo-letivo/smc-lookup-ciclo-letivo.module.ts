import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SmcLookupCicloLetivoComponent } from './smc-lookup-ciclo-letivo.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { SmcLookupModule } from '../smc-lookup/smc-lookup.module';
import { PoUiModule } from '../../../modules/po-ui.module';
import { SmcButtonModule } from '../../smc-button/smc-button.module';

@NgModule({
  declarations: [SmcLookupCicloLetivoComponent],
  imports: [
    CommonModule, SmcLookupModule, FormsModule, ReactiveFormsModule, PoUiModule, SmcButtonModule
  ],
  exports:[SmcLookupCicloLetivoComponent]
})
export class SmcLookupCicloLetivoModule { }
