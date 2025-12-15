import { SmcKeyValueModel } from 'projects/shared/models/smc-key-value.model';
import { EventoAulaModel } from './evento-aula.model';
export interface EventoAulaDivisaoTurmaModel {
  seq: string;
  turmaCodigo: number;
  turmaNumero: number;
  numero: number;
  cargaHorariaGrade?: number;
  numeroGrupo: number;
  descricaoLocalidade: string;
  tipoDivisaoDescricao: string;
  eventoAulas: EventoAulaModel[];
  grupoFormatado: string;
  descricaoDivisaoFormatada: string;
  temHistoricoEscolar: boolean;
  tipoDistribuicaoAula?: 'Semanal' | 'Quinzenal' | 'Livre' | 'Concentrada';
  tipoPulaFeriado?: 'Não Pula' | 'Pula Conjugado' | 'Pula Simples';
  aulaSabado?: boolean;
  inicioPeriodoLetivo?: string;
  fimPeriodoLetivo?: string;
  cargaHorariaLancada?: number;
  seqCursoOfertaLocalidadeTurno?: string;
  dscTabelaHorario?: string;
  quantidadeSemanas?: number;
  compartilhamentos: SmcKeyValueModel[];

  //Propriedades de controle
  visivel: boolean;
  cor: string;
}
