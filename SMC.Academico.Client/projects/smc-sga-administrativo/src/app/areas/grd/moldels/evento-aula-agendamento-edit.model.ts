import { EventoAulaAgendamentoProfessorModel } from './evento-aula-agendamento-professor.model';
export interface EventoAulaAgendamentoEditModel {
  seqDivisaoTurma: string;
  descricaoFormatada: string;
  descricaoEventoAulaDiaFormatado: string;
  cargaHorariaGrade: number;
  cargaHorariaLancada: number;
  inicioPeriodoLetivo: string;
  fimPeriodoLetivo: string;
  tipoDistribuicaoAula: string;
  tipoPulaFeriado: string;
  aulaSabado: boolean;
  codRecorrencia: string;

  idtTipoInicioRecorrencia?: number;
  idtTipoRecorrencia?: number;
  repetir?: number;
  diaSemana: string;
  turno: number;
  horaInicio: string;
  horaFim: string;
  comeca: string;
  termina: string;
  local: string;
  professores: EventoAulaAgendamentoProfessorModel[];
}
