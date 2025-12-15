import { SituacaoApuracaoFrequencia } from './apuracao-frequencia-types.model';

export interface ApuracaoFrequenciaAulaModel {
  seqEventoAula: string;
  descricaoFormatada: string;
  sigla: string;
  situacaoApuracaoFrequencia: SituacaoApuracaoFrequencia;
  data: Date;
  horaInicio: string;
  horaFim: string;
  dataPrimeiraApuracaoFrequencia?: Date;
  dataLimiteApuracaoFrequencia: Date;
  usuarioPrimeiraApuracaoFrequencia?: string;

  turnoAtual?: boolean;
  /**
   * Data da aula no formato YYYYMMDD
   */
  dataFormatada?: string;
  mostrar?: boolean;
  situacaoApuracaoFrequenciaOriginal?: SituacaoApuracaoFrequencia;
}
