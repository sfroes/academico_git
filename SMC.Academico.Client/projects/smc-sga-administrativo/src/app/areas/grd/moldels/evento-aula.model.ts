import { EventoAulaAgendamentoProfessorModel } from './evento-aula-agendamento-professor.model';
import { SituacaoApuracaoFrequencia } from './evento-aula-agendamento-types.model';

export interface EventoAulaModel {
  seq: string;
  seqDivisaoTurma: string;
  seqEventoAgd: string;
  seqHorarioAgd: string;
  data: Date;
  diaSemana: number;
  horaInicio: string;
  horaFim: string;
  turno: number;
  local: string;
  codigoRecorrencia: string;
  diaSemanaFormatada: string;
  diaSemanaDescricao: string;
  colaboradores: EventoAulaAgendamentoProfessorModel[];
  descricaoColaboradores?: string;
  codigoLocalSEF?: string;
  descricao?: string;
  situacaoApuracaoFrequencia: SituacaoApuracaoFrequencia;
  feriado: boolean;

  //Propriedades de controle
  grupoFormatado?: string;
  visivel: boolean;
  acoes: string[];
  cor: string;
}
