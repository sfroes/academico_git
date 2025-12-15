import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EventoAulaComponent } from './evento-aula.component';
import { SharedModule } from 'projects/smc-sga-administrativo/src/app/shared/shared.module';
import { EventoAulaFiltroComponent } from './evento-aula-filtro/evento-aula-filtro.component';
import { EventoAulaTurmaCabecalhoComponent } from './evento-aula-turma-cabecalho/evento-aula-turma-cabecalho.component';
import { EventoAulaDivisaoTurmaComponent } from './evento-aula-divisao-turma/evento-aula-divisao-turma.component';
import { SmcCalendarModule } from 'projects/shared/components/smc-calendar/smc-calendar.module';
import { SmcButtonModule } from 'projects/shared/components/smc-button/smc-button.module';
import { EventoAulaCalendarioComponent } from './evento-aula-calendario/evento-aula-calendario.component';
import { EventoAulaAgendamentoAddComponent } from './evento-aula-agendamento-add/evento-aula-agendamento-add.component';
import { SmcLookupTurmaModule } from 'projects/shared/components/lookups/lk-tur-001-turma/smc-lookup-turma.module';
import { EventoAulaListaDivisaoTurmaComponent } from './evento-aula-lista-divisao-turma/evento-aula-lista-divisao-turma.component';
import { EventoAulaNotificacoesComponent } from './evento-aula-notificacoes/evento-aula-notificacoes.component';
import { SmcTableModule } from '../../../../../../shared/components/smc-table/smc-table.module';
import { SmcModalModule } from '../../../../../../shared/components/smc-modal/smc-modal.module';
import { EventoAulaAgendamentoDeleteComponent } from './evento-aula-agendamento-delete/evento-aula-agendamento-delete.component';
import { SmcSelectModule } from '../../../../../../shared/components/smc-select/smc-select.module';
import { EventoAulaAgendamentoDetalhesComponent } from './evento-aula-agendamento-detalhes/evento-aula-agendamento-detalhes.component';
import { EventoAulaAgendamentoSimulacaoComponent } from './evento-aula-agendamento-simulacao/evento-aula-agendamento-simulacao.component';
import { PipeModule } from '../../../../../../shared/pipe/pipe.module';
import { EventoAulaListarComponent } from './evento-aula-listar/evento-aula-listar.component';
import { EventoAulaIndexComponent } from './evento-aula-index/evento-aula-index.component';
import { EventoAulaRoutingModule } from './evento-aula-routing.module';
import { EventoAulaAgendamentoAddReduzidoComponent } from './evento-aula-agendamento-add-reduzido/evento-aula-agendamento-add-reduzido.component';
import { SmcLookupColaboradorModule } from '../../../../../../shared/components/lookups/lk-dct-001-colaborador/smc-lookup-colaborador.module';
import { BiGrd001Component } from '../blocos-interface/bi-grd-001/bi-grd-001.component';
import { SmcTreeViewModule } from '../../../../../../shared/components/smc-tree-view/smc-tree-view.module';
import { EventoAulaBiTreeLocalComponent } from './evento-aula-bi-tree-local/evento-aula-bi-tree-local.component';
import { EventoAulaAgendamentoEditHorarioComponent } from './evento-aula-agendamento-edit-horario/evento-aula-agendamento-edit-horario.component';
import { EventoAulaAgendamentoEditColaboradorComponent } from './evento-aula-agendamento-edit-colaborador/evento-aula-agendamento-edit-colaborador.component';
import { EventoAulaAgendamentoEditColaboradorSubstitutoComponent } from './evento-aula-agendamento-edit-colaborador-substituto/evento-aula-agendamento-edit-colaborador-substituto.component';
import { EventoAulaAgendamentoEditLocalComponent } from './evento-aula-agendamento-edit-local/evento-aula-agendamento-edit-local.component';
import { SmcLookupCicloLetivoModule } from './../../../../../../shared/components/lookups/lk-cam-002-ciclo-letivo/smc-lookup-ciclo-letivo.module';
import { EventoAulaAgendamentoAssociarProfessorComponent } from './evento-aula-agendamento-associar-professor/evento-aula-agendamento-associar-professor.component';

@NgModule({
  declarations: [
    EventoAulaComponent,
    EventoAulaFiltroComponent,
    EventoAulaTurmaCabecalhoComponent,
    EventoAulaDivisaoTurmaComponent,
    EventoAulaCalendarioComponent,
    EventoAulaAgendamentoAddComponent,
    EventoAulaListaDivisaoTurmaComponent,
    EventoAulaNotificacoesComponent,
    EventoAulaAgendamentoDeleteComponent,
    EventoAulaAgendamentoDetalhesComponent,
    EventoAulaAgendamentoSimulacaoComponent,
    EventoAulaListarComponent,
    EventoAulaIndexComponent,
    EventoAulaAgendamentoAddReduzidoComponent,
    BiGrd001Component,
    EventoAulaBiTreeLocalComponent,
    EventoAulaAgendamentoEditHorarioComponent,
    EventoAulaAgendamentoEditColaboradorComponent,
    EventoAulaAgendamentoEditColaboradorSubstitutoComponent,
    EventoAulaAgendamentoEditLocalComponent,
    EventoAulaAgendamentoAssociarProfessorComponent,
  ],
  imports: [
    SharedModule,
    CommonModule,
    SmcCalendarModule,
    SmcButtonModule,
    SmcLookupTurmaModule,
    SmcTableModule,
    SmcModalModule,
    SmcSelectModule,
    PipeModule,
    EventoAulaRoutingModule,
    SmcLookupColaboradorModule,
    SmcTreeViewModule,
    SmcLookupCicloLetivoModule,
  ],
  exports: [EventoAulaComponent],
})
export class EventoAulaModule {}
