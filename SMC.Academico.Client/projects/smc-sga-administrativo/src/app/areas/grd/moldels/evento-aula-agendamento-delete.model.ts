export interface EventoAulaAgendamentoDeleteModel {
  seqDivisaoTurma: string;
  seqEventoAula: string;
  seqHorarioAgd: string;
  descricaoFormatada: string;
  cargaHorariaGrade: number;
  cargaHorariaLancada: number;
  inicioPeriodoLetivo: string;
  fimPeriodoLetivo: string;
  tipoDistribuicaoAula: string;
  tipoPulaFeriado: string;
  aulaSabado: boolean;
  dscTabelaHorario: string;
  codigoRecorrencia: string;
  diaSemanaFormatada: string;
  diaSemanaDescricao: string;
  horaInicio: string;
  horaFim: string;
  codigoLocalSEF?: string;
  local: string;
  professores: string[];
  contemProfessores: boolean;
}
