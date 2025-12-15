export interface SituacaoAulaLoteFiltroModel {
  seqTurma?: Number;
  seqsDivisaoTurma: string[];
  seqsColaborador: Number[];
  situacaoApuracaoFrequencia?: Number;
  inicioPeriodo?: Date;
  fimPeriodo?: Date;
  seqCicloLetivo?: Number;
  dentroPerido?: Boolean;
}
