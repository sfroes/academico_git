import { EventoAulaModel } from './evento-aula.model';
export interface EventoAulaAgendamentoAddModel {
  seqDivisaoTurma: string;
  eventos: EventoAulaModel[];
}
