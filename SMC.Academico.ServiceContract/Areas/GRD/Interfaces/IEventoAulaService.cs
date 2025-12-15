using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.ServiceContract.Areas.GRD.Data;
using SMC.Framework.Service;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.GRD.Interfaces
{
    public interface IEventoAulaService : ISMCService
    {
        EventoAulaTurmaData BuscarEventosTurma(long seqTurma);
        EventoAulaDivisaoTurmaData BuscarEventosOrigemAvaliacao(long? seqDivisaoTurma, long? seqOrigemAvaliacao);
        void SalvarEventos(List<EventoAulaData> eventos);
        void EditarEventos(List<EventoAulaData> eventos, List<long> seqsEventosExcluir);
        void EditarLocalEventos(int? codigoLocalSEF, string local, List<long> seqEventos);
        void EditarColaboradoresEventos(List<EventoAulaColaboradorData> colaboradores, List<long> seqEventos, long seqEventoTemplate, bool somenteColaborador);
        void ExcluirEventos(List<long> seqsEventoAula);
        string ValidarColisao(List<EventoAulaValidacaoColisaoColaboradorData> model);
        bool ValidarVinculoColaboradorPeriodo(long seqColaborador, DateTime dataInicio, DateTime dataFim);
        List<string> ValidarColisaoHorarioAluno(List<EventoAulaData> eventos);
        List<string> ValidarColisaoHorarioSolicitacaoServico(List<EventoAulaData> eventos);
        /// <summary>
        /// Valida as colisões de horários entre as divisões de turma informadas
        /// </summary>
        /// <param name="seqsDivisaoTurma">Seq divisões de turma a serem avaliadas</param>
        /// <returns>Seqs das divisões que colidirem</returns>
        List<long> ValidarColisaoHorariosDivisoes(List<long> seqsDivisaoTurma);
        /// <summary>
        /// Buscar eventos aula em lote
        /// </summary>
        /// <param name="filtro">Filtros da pesquisa</param>
        /// <returns>Lista de eventos aula lote</returns>
        List<EventoAulaLoteData> BuscarEventosAulaLote(EventoAulaFiltroData filtro);
        /// <summary>
        /// Liberar eventos aula para novas apurações, alterando para uma nova data valida
        /// </summary>
        /// <param name="seqsEventoAula">Sequenciais dos eventos aula</param>
        void LiberarEventosAulaApuracao(List<long> seqsEventoAula);
        /// <summary>
        /// Liberar eventos aula para correção, alterando o prazo de alteração
        /// </summary>
        /// <param name="seqsEventoAula">Sequenciais dos eventos aula</param>
        void LiberarEventosAulaCorrecao(List<long> seqsEventoAula);
        /// <summary>
        /// Alterar situação do evento aula para "não executada" ou "não apurada"
        /// </summary>
        /// <param name="seqsEventoAula">Sequenciais dos eventos aula</param>
        /// <param name="situacaoApuracaoFrequencia">Situação para a qual o evneto será alterada</param>
        void AlterarEventosAulasNaoExecutadaOuNaoApurada(List<long> seqsEventoAula, SituacaoApuracaoFrequencia situacaoApuracaoFrequencia);    }
}
