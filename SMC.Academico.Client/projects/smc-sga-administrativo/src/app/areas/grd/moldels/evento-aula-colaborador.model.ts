import { EventoAulaColaboradorVinculosModel } from './evento-aula-colaborador-vinculos.model';
export interface EventoAulaColaboradorModel {
  seq: string;
  nome: string;
  nomeFormatado: string;
  vinculos: EventoAulaColaboradorVinculosModel[];
}
