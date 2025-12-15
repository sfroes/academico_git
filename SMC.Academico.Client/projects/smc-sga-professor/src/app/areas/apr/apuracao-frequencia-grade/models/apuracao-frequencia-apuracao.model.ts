import { Frequencia, OcorrenciaFrequencia } from './apuracao-frequencia-types.model';

export interface ApuracaoFrequenciaApuracaoModel {
  seq: string;
  seqEventoAula: string;
  frequencia?: Frequencia;
  observacao?: string;
  dataObservacao?: string;
  ocorrenciaFrequencia: OcorrenciaFrequencia;
  descricaoTipoMensagem: string;

  mostrar?: boolean;
}
