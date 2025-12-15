import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedModule } from '../../../shared/shared.module';
import { MultiSelectModule } from 'primeng/multiselect';
import { InputTextModule } from 'primeng/inputtext';
import { InputNumberModule } from 'primeng/inputnumber';
import { ApuracaoFrequenciaCabecalhoComponent } from './apuracao-frequencia-cabecalho/apuracao-frequencia-cabecalho.component';
import { ApuracaoFrequenciaFiltroComponent } from './apuracao-frequencia-filtro/apuracao-frequencia-filtro.component';
import { ApuracaoFrequenciaGridHeaderAulaComponent } from './apuracao-frequencia-grid-header-aula/apuracao-frequencia-grid-header-aula.component';
import { ApuracaoFrequenciaGridHeaderComponent } from './apuracao-frequencia-grid-header/apuracao-frequencia-grid-header.component';
import { ApuracaoFrequenciaGridItemApuracaoComponent } from './apuracao-frequencia-grid-item-apuracao/apuracao-frequencia-grid-item-apuracao.component';
import { ApuracaoFrequenciaGridItensComponent } from './apuracao-frequencia-grid-itens/apuracao-frequencia-grid-itens.component';
import { ApuracaoFrequenciaGridComponent } from './apuracao-frequencia-grid/apuracao-frequencia-grid.component';
import { ApuracaoFrequenciaLegendaComponent } from './apuracao-frequencia-legenda/apuracao-frequencia-legenda.component';
import { ApuracaoFrequenciaComponent } from './apuracao-frequencia.component';
import { PoUiModule } from 'projects/shared/modules/po-ui.module';
import { ApuracaoFrequenciaNotificacoesComponent } from './apuracao-frequencia-notificacoes/apuracao-frequencia-notificacoes.component';

@NgModule({
  declarations: [
    ApuracaoFrequenciaComponent,
    ApuracaoFrequenciaCabecalhoComponent,
    ApuracaoFrequenciaFiltroComponent,
    ApuracaoFrequenciaGridComponent,
    ApuracaoFrequenciaGridHeaderComponent,
    ApuracaoFrequenciaGridItensComponent,
    ApuracaoFrequenciaLegendaComponent,
    ApuracaoFrequenciaGridHeaderAulaComponent,
    ApuracaoFrequenciaGridItemApuracaoComponent,
    ApuracaoFrequenciaNotificacoesComponent,
  ],
  imports: [CommonModule, SharedModule, MultiSelectModule, InputTextModule, InputNumberModule, PoUiModule],
})
export class ApuracaoFrequenciaGradeModule {}
