import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SmcLookupModule } from '../smc-lookup/smc-lookup.module';
import { PoUiModule } from 'projects/shared/modules/po-ui.module';
import { SmcButtonModule } from '../../smc-button/smc-button.module';
import { SmcLookupCursoOfertaLocalidadeComponent } from './smc-lookup-curso-oferta-localidade.component';
import { SmcSelectModule } from '../../smc-select/smc-select.module';



@NgModule({
  declarations: [SmcLookupCursoOfertaLocalidadeComponent],
  imports: [
    CommonModule,
    SmcLookupModule,
    SmcSelectModule,
    FormsModule,
    ReactiveFormsModule,
    PoUiModule
  ],
  exports:[SmcLookupCursoOfertaLocalidadeComponent]
})
export class SmcLookupCursoOfertaLocalidadeModule {}
