import { EventoAulaAgendamentoHorarioModel } from './evento-aula-agendamento-horario.model';
export interface EventoAulaAgendamentoTabelaHorarioModel {
  seq: string;
  seqTipoCalendario: string;
  descricao: string;
  ativo: boolean;
  padrao: boolean;
  horarios: EventoAulaAgendamentoHorarioModel[];
}
