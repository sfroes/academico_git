import { EventoAulaTurmaCabecalhoModel } from './evento-aula-turma-cabecalho.model';
import { EventoAulaDivisaoTurmaModel } from './evento-aula-divisao-turma.model';

export interface EventoAulaTurmaModel {
  eventoAulaTurmaCabecalho: EventoAulaTurmaCabecalhoModel;

  eventoAulaDivisoesTurma: EventoAulaDivisaoTurmaModel[];

  //Token's
  permiteAlterarDataAgendamentoAula: boolean;
}
