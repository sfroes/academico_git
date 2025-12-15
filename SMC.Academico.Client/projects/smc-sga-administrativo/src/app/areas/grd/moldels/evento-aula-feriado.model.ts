export interface EventoAulaFeriadoModel {
  codigoUnidade: number;
  dataInicio: Date;
  dataFim: Date;
  seqEvento: number;
  descricaoEvento: string;
  naoAula: boolean;
}
