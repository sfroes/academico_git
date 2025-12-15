import { EventoAulaTurmaCabecalhoConfiguracoesrModel } from '../moldels/evento-aula-turma-cabecalho-configuracoes.model';
import { EventoAulaTurmaColaboradoresAssociarProfessorModel } from '../moldels/evento-aula-turma-colaboradores-associar-professor.model';

export interface EventoAulaTurmaCabecalhoAssociarProfessorModel {
  codigoFormatado: string;
  cicloLetivoInicio: string;
  vagas: string;
  descricaoTipoTurma: string;
  situacaoTurmaAtual: string;
  configuacoesComponente: EventoAulaTurmaCabecalhoConfiguracoesrModel[];
  colaboradoresTurma: EventoAulaTurmaColaboradoresAssociarProfessorModel;
}
