import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PoUiModule } from 'projects/shared/modules/po-ui.module';
import { SmcButtonModule } from 'projects/shared/components/smc-button/smc-button.module';
import { FormsModule } from '@angular/forms';
import { SmcLookupComponent } from './smc-lookup.component';
import { SmcLookupBuscaSimplesComponent } from './smc-lookup-busca-simples/smc-lookup-busca-simples.component';
import { SmcLookupBuscaAvancadaComponent } from './smc-lookup-busca-avancada/smc-lookup-busca-avancada.component';
import { SmcTableModule } from "../../smc-table/smc-table.module";

@NgModule({
  declarations: [SmcLookupBuscaSimplesComponent, SmcLookupBuscaAvancadaComponent, SmcLookupComponent],
  imports: [CommonModule, PoUiModule, FormsModule, SmcButtonModule, SmcTableModule],
  exports: [SmcLookupComponent],
})
export class SmcLookupModule {}
