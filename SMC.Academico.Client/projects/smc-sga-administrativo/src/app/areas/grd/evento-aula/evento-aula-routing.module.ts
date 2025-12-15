import { NgModule } from '@angular/core';
import { RouterModule, Routes, Router } from '@angular/router';
import { EventoAulaComponent } from './evento-aula.component';
import { EventoAulaListarComponent } from './evento-aula-listar/evento-aula-listar.component';
import { EventoAulaIndexComponent } from './evento-aula-index/evento-aula-index.component';
import { EventoAulaAgendamentoAddComponent } from './evento-aula-agendamento-add/evento-aula-agendamento-add.component';
import { EventoAulaAgendamentoDeleteComponent } from './evento-aula-agendamento-delete/evento-aula-agendamento-delete.component';
import { EventoAulaAgendamentoDetalhesComponent } from './evento-aula-agendamento-detalhes/evento-aula-agendamento-detalhes.component';
import { EventoAulaAgendamentoAddReduzidoComponent } from './evento-aula-agendamento-add-reduzido/evento-aula-agendamento-add-reduzido.component';
import { EventoAulaAgendamentoEditHorarioComponent } from './evento-aula-agendamento-edit-horario/evento-aula-agendamento-edit-horario.component';
import { EventoAulaAgendamentoEditColaboradorComponent } from './evento-aula-agendamento-edit-colaborador/evento-aula-agendamento-edit-colaborador.component';
import { EventoAulaAgendamentoEditLocalComponent } from './evento-aula-agendamento-edit-local/evento-aula-agendamento-edit-local.component';
import { EventoAulaAgendamentoEditColaboradorSubstitutoComponent } from './evento-aula-agendamento-edit-colaborador-substituto/evento-aula-agendamento-edit-colaborador-substituto.component';
import { EventoAulaAgendamentoAssociarProfessorComponent } from './evento-aula-agendamento-associar-professor/evento-aula-agendamento-associar-professor.component';

const eventoAulaRoutes: Routes = [
  {
    path: 'GRD/EventoAula',
    component: EventoAulaComponent,
    children: [
      {
        path: '',
        pathMatch: 'full',
        redirectTo: 'Index',
      },
      {
        path: 'Turma/:seqTurma',
        component: EventoAulaListarComponent,
      },
      {
        path: 'Index/:seqTurma',
        component: EventoAulaIndexComponent,
      },
      {
        path: 'Index',
        component: EventoAulaIndexComponent,
      },
    ],
  },
  {
    path: 'Add',
    component: EventoAulaAgendamentoAddComponent,
    outlet: 'modais',
  },
  {
    path: 'AddReduce/:dataInicio',
    component: EventoAulaAgendamentoAddReduzidoComponent,
    outlet: 'modais',
  },
  {
    path: 'Detalhes/:seqEventoAula/:seqDivisaoTurma',
    component: EventoAulaAgendamentoDetalhesComponent,
    outlet: 'modais',
  },
  {
    path: 'EditHorario/:seqEventoAula/:seqDivisaoTurma',
    component: EventoAulaAgendamentoEditHorarioComponent,
    outlet: 'modais',
  },
  {
    path: 'EditColaborador/:seqEventoAula/:seqDivisaoTurma',
    component: EventoAulaAgendamentoEditColaboradorComponent,
    outlet: 'modais',
  },
  {
    path: 'EditColaboradorSubstituto/:seqEventoAula/:seqDivisaoTurma',
    component: EventoAulaAgendamentoEditColaboradorSubstitutoComponent,
    outlet: 'modais',
  },
  {
    path: 'EditLocal/:seqEventoAula/:seqDivisaoTurma',
    component: EventoAulaAgendamentoEditLocalComponent,
    outlet: 'modais',
  },
  {
    path: 'Delete/:seqEventoAula/:seqDivisaoTurma',
    component: EventoAulaAgendamentoDeleteComponent,
    outlet: 'modais',
  },
  {
    path: 'AssociarProfessor',
    component: EventoAulaAgendamentoAssociarProfessorComponent,
    outlet: 'modais',
  },
];
@NgModule({
  imports: [RouterModule.forChild(eventoAulaRoutes)],
  exports: [RouterModule],
})
export class EventoAulaRoutingModule {}
