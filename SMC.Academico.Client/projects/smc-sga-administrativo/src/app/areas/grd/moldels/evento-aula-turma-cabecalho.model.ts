export interface EventoAulaTurmaCabecalhoModel {
  seqTurma: string;
  seqCicloLetivoInicio: string;
  cicloLetivoInicio: string;
  codigoFormatado: string;
  descricaoConfiguracaoComponente: string;
  inicioPeriodoLetivo: string;
  fimPeriodoLetivo: string;
  somenteLeitura: boolean;
  mensagemFalha: string;
  codigoUnidadeSeo?: number;
  seqAgendaTurma?: number;
  tokenTipoEventoAula: string;
  seqCursoOfertaLocalidade: string;

  // Campos calculados no front
  cargaHorariaCompleta?: boolean;
}
