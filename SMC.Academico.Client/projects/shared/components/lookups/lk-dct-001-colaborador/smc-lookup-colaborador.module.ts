import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SmcLookupColaboradorComponent } from './smc-lookup-colaborador.component';
import { SmcLookupModule } from '../smc-lookup/smc-lookup.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PoUiModule } from 'projects/shared/modules/po-ui.module';
import { SmcButtonModule } from '../../smc-button/smc-button.module';
import { SmcSelectModule } from '../../smc-select/smc-select.module';
import { SmcLookupInstituicaoExternaModule } from '../lk-dct-002-instituicao-externa/smc-lookup-instituicao-externa.module';
import { SmcLookupCursoOfertaLocalidadeModule } from '../lk-cso-005-oferta-curso-localidade/smc-lookup-curso-oferta-localidade.module';

@NgModule({
  declarations: [SmcLookupColaboradorComponent],
  imports: [
    CommonModule,
    SmcLookupModule,
    FormsModule,
    ReactiveFormsModule,
    PoUiModule,
    SmcButtonModule,
    SmcSelectModule,
    SmcLookupInstituicaoExternaModule,
    SmcLookupCursoOfertaLocalidadeModule,
  ],
  exports: [SmcLookupColaboradorComponent],
})
export class SmcLookupColaboradorModule {}
