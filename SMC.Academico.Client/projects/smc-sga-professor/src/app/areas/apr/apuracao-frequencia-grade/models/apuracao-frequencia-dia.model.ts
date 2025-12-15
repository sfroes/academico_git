import { ApuracaoFrequenciaAulaModel } from "./apuracao-frequencia-aula.model";

export interface ApuracaoFrequenciaDiaModel {
  descricaoFormatada: string;
  mostrar: boolean;
  data: Date;
  aulas: ApuracaoFrequenciaAulaModel[];
}
