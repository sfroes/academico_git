using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Domain.Areas.GRD.DomainServices;
using SMC.Academico.Domain.Areas.GRD.Models;
using SMC.Academico.Domain.Areas.GRD.ValueObjects;
using SMC.Academico.ServiceContract.Areas.GRD.Data;
using SMC.Academico.ServiceContract.Areas.GRD.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Service;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.GRD.Services
{
    public class EventoAulaService : SMCServiceBase, IEventoAulaService
    {
        #region [ DomainServices ]

        private EventoAulaDomainService EventoAulaDomainService => Create<EventoAulaDomainService>();

        #endregion [ DomainServices ]

        public EventoAulaTurmaData BuscarEventosTurma(long seqTurma)
        {
            return this.EventoAulaDomainService.BuscarEventosTurma(seqTurma).Transform<EventoAulaTurmaData>();
        }

        public EventoAulaDivisaoTurmaData BuscarEventosOrigemAvaliacao(long? seqDivisaoTurma, long? seqOrigemAvaliacao)
        {
            return this.EventoAulaDomainService.BuscarEventosOrigemAvaliacao(seqDivisaoTurma, seqOrigemAvaliacao).Transform<EventoAulaDivisaoTurmaData>();
        }

        public void SalvarEventos(List<EventoAulaData> eventos)
        {
            this.EventoAulaDomainService.SalvarEventos(eventos.TransformList<EventoAulaVO>());
        }

        public void EditarEventos(List<EventoAulaData> eventos, List<long> seqsEventosExcluir)
        {
            this.EventoAulaDomainService.EditarEventos(eventos.TransformList<EventoAulaVO>(), seqsEventosExcluir);
        }

        public void EditarLocalEventos(int? codigoLocalSEF, string local, List<long> seqEventos)
        {
            this.EventoAulaDomainService.EditarLocalEventos(codigoLocalSEF, local, seqEventos);
        }

        public void EditarColaboradoresEventos(List<EventoAulaColaboradorData> colaboradores, List<long> seqEventos, long seqEventoTemplate, bool somenteColaborador)
        {
            this.EventoAulaDomainService.EditarColaboradoresEventos(colaboradores.TransformList<EventoAulaColaborador>(), seqEventos, seqEventoTemplate, somenteColaborador);
        }

        public void ExcluirEventos(List<long> seqsEventoAula)
        {
            this.EventoAulaDomainService.ExcluirEventos(seqsEventoAula);
        }

        public string ValidarColisao(List<EventoAulaValidacaoColisaoColaboradorData> model)
        {
            return this.EventoAulaDomainService.ValidarColisao(model.TransformList<EventoAulaValidacaoColisaoColaboradorVO>());
        }

        public bool ValidarVinculoColaboradorPeriodo(long seqColaborador, DateTime dataInicio, DateTime dataFim)
        {
            return this.EventoAulaDomainService.ValidarVinculoColaboradorPeriodo(seqColaborador, dataInicio, dataFim);
        }

        public List<string> ValidarColisaoHorarioAluno(List<EventoAulaData> eventos)
        {
            return this.EventoAulaDomainService.ValidarColisaoHorarioAluno(eventos.TransformList<EventoAulaVO>());
        }

        public List<string> ValidarColisaoHorarioSolicitacaoServico(List<EventoAulaData> eventos)
        {
            return this.EventoAulaDomainService.ValidarColisaoHorarioSolicitacaoServico(eventos.TransformList<EventoAulaVO>());
        }

        /// <summary>
        /// Valida as colisões de horários entre as divisões de turma informadas
        /// </summary>
        /// <param name="seqsDivisaoTurma">Seq divisões de turma a serem avaliadas</param>
        /// <returns>Seqs das divisões que colidirem</returns>
        public List<long> ValidarColisaoHorariosDivisoes(List<long> seqsDivisaoTurma)
        {
            return this.EventoAulaDomainService.ValidarColisaoHorariosDivisoes(seqsDivisaoTurma);
        }

        /// <summary>
        /// Buscar eventos aula em lote
        /// </summary>
        /// <param name="filtro">Filtros da pesquisa</param>
        /// <returns>Lista de eventos aula lote</returns>
        public List<EventoAulaLoteData> BuscarEventosAulaLote(EventoAulaFiltroData filtro)
        {
            return EventoAulaDomainService.BuscarEventosAulaLote(filtro.Transform<EventoAulaFiltroVO>()).TransformList<EventoAulaLoteData>();
        }

        /// <summary>
        /// Liberar eventos aula para novas apurações, alterando para uma nova data valida
        /// </summary>
        /// <param name="seqsEventoAula">Sequenciais dos eventos aula</param>
        public void LiberarEventosAulaApuracao(List<long> seqsEventoAula)
        {
            EventoAulaDomainService.LiberarEventosAulaApuracao(seqsEventoAula);
        }

        /// <summary>
        /// Liberar eventos aula para correção, alterando o prazo de alteração
        /// </summary>
        /// <param name="seqsEventoAula">Sequenciais dos eventos aula</param>
        public void LiberarEventosAulaCorrecao(List<long> seqsEventoAula)
        {
            EventoAulaDomainService.LiberarEventosAulaCorrecao(seqsEventoAula);
        }

        /// <summary>
        /// Alterar situação do evento aula para "não executada" ou "não apurada"
        /// </summary>
        /// <param name="seqsEventoAula">Sequenciais dos eventos aula</param>
        /// <param name="situacaoApuracaoFrequencia">Situação para a qual o evneto será alterada</param>
        public void AlterarEventosAulasNaoExecutadaOuNaoApurada(List<long> seqsEventoAula, SituacaoApuracaoFrequencia situacaoApuracaoFrequencia)
        {
            EventoAulaDomainService.AlterarEventosAulasNaoExecutadaOuNaoApurada(seqsEventoAula, situacaoApuracaoFrequencia);
        }
    }
}