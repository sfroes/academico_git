import { ApuracaoFrequenciaApuracaoModel } from './apuracao-frequencia-apuracao.model';

export interface ApuracaoFrequenciaAlunoModel {
  seqAlunoHistoricoCicloLetivo: string;
  numeroRegistroAcademico: string;
  nome: string;
  alunoFormado: boolean;
  alunoHistoricoEscolar: boolean;
  apuracoes: ApuracaoFrequenciaApuracaoModel[];

  mostrar?: boolean;
}
