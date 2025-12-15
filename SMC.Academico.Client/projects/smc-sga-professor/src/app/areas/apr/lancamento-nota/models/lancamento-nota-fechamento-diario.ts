import { LancamentoNotaDiarioAluno } from './lancamento-nota-diario-aluno';

export interface LancamentoNotaDiario {
  seqOrigemAvaliacao: string;
  materiaLecionada: string;
  fechamentoIndividual: boolean;
  alunos: LancamentoNotaDiarioAluno[];
}
