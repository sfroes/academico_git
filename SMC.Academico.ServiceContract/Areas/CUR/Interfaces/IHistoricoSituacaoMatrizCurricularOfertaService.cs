using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Interfaces
{
    public interface IHistoricoSituacaoMatrizCurricularOfertaService : ISMCService
    {
        /// <summary>
        /// Lista as opções de situação da matriz curricular de acordo com a regra RN_CUR_057 Cadastro/alteração situação matriz curricular
        /// Em ativação <> Ativa <> Em extinção <> Extinta
        /// </summary>
        /// <param name="seqMatrizCurricularOferta">Sequencial da matriz curricular oferta</param>
        /// <returns>Lista de opções da situação</returns>
        List<SMCDatasourceItem> SituacoesMatrizCurricularOferta(long seqMatrizCurricularOferta);

        /// <summary>
        /// Busca os históricos de situações matriz curricular oferta acordo com os filtros
        /// </summary>
        /// <param name="filtros">Objeto históricos filtro</param>
        /// <returns>SMCPagerData com a lista de históricos</returns>
        SMCPagerData<HistoricoSituacaoMatrizCurricularOfertaData> BuscarHistoricosSituacoesMatrizCurricularOferta(HistoricoSituacaoMatrizCurricularOfertaFiltroData filtro);

        /// <summary>
        /// Salva uma nova situação da matriz curricular e edita a anterior preenchendo a data final
        /// </summary>
        /// <param name="historicoSituacaoMatrizCurricularOferta">Dados do registro a ser gravado</param>
        /// <returns>Sequencial do registro gravado</returns>
        long SalvarHistoricoSituacaoMatrizCurricularOferta(HistoricoSituacaoMatrizCurricularOfertaData historicoSituacaoMatrizCurricularOferta);

        /// <summary>
        /// Exclui ó último histórico situação da matriz curricular e edita a anterior preenchendo a data final como null
        /// </summary>
        /// <param name="seq">Sequencial do registro</param>
        void ExcluirHistoricoSituacaoMatrizCurricularOferta(long seq);
    }
}
