import { ApuracaoFrequenciaAlunoModel } from './apuracao-frequencia-aluno.model';
import { ApuracaoFrequenciaAulaModel } from './apuracao-frequencia-aula.model';
import { ApuracaoFrequenciaDiaModel } from './apuracao-frequencia-dia.model';

export interface ApuracaoFrequenciaModel {
  descricaoOrigemAvaliacao: string;
  dataLimite: Date;
  quantidadeMinutosPrazoAlteracaoFrequencia: number;
  quantidadeDiasPrazoApuracaoFrequencia: number;
  cargaHoraria: number;
  usuarioAutenticado: string;
  aulas: ApuracaoFrequenciaAulaModel[];
  alunos: ApuracaoFrequenciaAlunoModel[];

  dias?: ApuracaoFrequenciaDiaModel[];
}
