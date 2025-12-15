using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.CUR.Services
{
    public class HistoricoSituacaoMatrizCurricularOfertaService : SMCServiceBase, IHistoricoSituacaoMatrizCurricularOfertaService
    {
        #region [ DomainService ]

        private HistoricoSituacaoMatrizCurricularOfertaDomainService HistoricoSituacaoMatrizCurricularOfertaDomainService
        {
            get { return this.Create<HistoricoSituacaoMatrizCurricularOfertaDomainService>(); }
        }

        #endregion

        /// <summary>
        /// Lista as opções de situação da matriz curricular de acordo com a regra RN_CUR_057 Cadastro/alteração situação matriz curricular
        /// Em ativação <-> Ativa <-> Em extinção <-> Extinta
        /// </summary>
        /// <param name="seqMatrizCurricularOferta">Sequencial da matriz curricular oferta</param>
        /// <returns>Lista de opções da situação</returns>
        public List<SMCDatasourceItem> SituacoesMatrizCurricularOferta(long seqMatrizCurricularOferta)
        {
            return HistoricoSituacaoMatrizCurricularOfertaDomainService.SituacoesMatrizCurricularOferta(seqMatrizCurricularOferta);
        }

        /// <summary>
        /// Busca os históricos de situações matriz curricular oferta acordo com os filtros
        /// </summary>
        /// <param name="filtro">Objeto históricos filtro</param>
        /// <returns>SMCPagerData com a lista de históricos</returns>
        public SMCPagerData<HistoricoSituacaoMatrizCurricularOfertaData> BuscarHistoricosSituacoesMatrizCurricularOferta(HistoricoSituacaoMatrizCurricularOfertaFiltroData filtro)
        {            
            return HistoricoSituacaoMatrizCurricularOfertaDomainService.BuscarHistoricosSituacoesMatrizCurricularOferta(filtro.Transform<HistoricoSituacaoMatrizCurricularOfertaFilterSpecification>())
                .Transform<SMCPagerData<HistoricoSituacaoMatrizCurricularOfertaData>>();
        }

        /// <summary>
        /// Salva uma nova situação da matriz curricular e edita a anterior preenchendo a data final
        /// </summary>
        /// <param name="historicoSituacaoMatrizCurricularOferta">Dados do registro a ser gravado</param>
        /// <returns>Sequencial do registro gravado</returns>
        public long SalvarHistoricoSituacaoMatrizCurricularOferta(HistoricoSituacaoMatrizCurricularOfertaData historicoSituacaoMatrizCurricularOferta)
        {
            return HistoricoSituacaoMatrizCurricularOfertaDomainService.SalvarHistoricoSituacaoMatrizCurricularOferta(historicoSituacaoMatrizCurricularOferta.Transform<HistoricoSituacaoMatrizCurricularOferta>());
        }

        /// <summary>
        /// Exclui ó último histórico situação da matriz curricular e edita a anterior preenchendo a data final como null
        /// </summary>
        /// <param name="seq">Sequencial do registro</param>
        public void ExcluirHistoricoSituacaoMatrizCurricularOferta(long seq)
        {
            HistoricoSituacaoMatrizCurricularOfertaDomainService.ExcluirHistoricoSituacaoMatrizCurricularOferta(seq);
        }
    }
}
