import { EventoAulaAgendamentoProfessorModel } from './evento-aula-agendamento-professor.model';

export interface EventoAulaSimulacaoModel {
  seqDivisaoTurma: string;
  seqsHorarios: string[];
  descricaoDivisao: string;
  idtTipoRecorrencia: number;
  idtTipoInicioRecorrencia?: number;
  comeca: string;
  termina: string;
  repetir: string;
  local: string;
  codigoLocalSEF?: string;
  colaboradores: EventoAulaAgendamentoProfessorModel[];
}
