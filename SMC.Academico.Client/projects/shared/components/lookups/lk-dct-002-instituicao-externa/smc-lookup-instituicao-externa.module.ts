import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SmcLookupInstituicaoExternaComponent } from './smc-lookup-instituicao-externa.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { SmcLookupModule } from '../smc-lookup/smc-lookup.module';
import { PoUiModule } from '../../../modules/po-ui.module';
import { SmcButtonModule } from '../../smc-button/smc-button.module';
import { SmcSelectModule } from '../../smc-select/smc-select.module';

@NgModule({
  declarations: [SmcLookupInstituicaoExternaComponent],
  imports: [
    CommonModule, SmcLookupModule, FormsModule, ReactiveFormsModule, PoUiModule, SmcButtonModule, SmcSelectModule
  ],
  exports:[SmcLookupInstituicaoExternaComponent]
})
export class SmcLookupInstituicaoExternaModule { }
