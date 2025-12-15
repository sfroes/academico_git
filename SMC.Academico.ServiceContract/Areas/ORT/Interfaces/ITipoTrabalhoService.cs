using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Academico.ServiceContract.Areas.ORT.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.ORT.Interfaces
{
    public interface ITipoTrabalhoService : ISMCService
    {
        List<SMCDatasourceItem> BuscarTiposTrabalhoSelect(long seqInstituicaoEnsino);

        /// <summary>
        /// Busca os tipos de trabalho que tem a flag PublicacaoBibliotecaObrigatoria para acesso do BDP
        /// </summary>
        /// <param name="seqInstituicaoEnsino">Sequencial da instituição de ensino</param>
        /// <returns>Lista com os tipos de trabalho</returns>
        List<SMCDatasourceItem> BuscarTiposTrabalhoSelectBDP(long seqInstituicaoEnsino);

        /// <summary>
        /// Busca os tipos de trabalho para uma instituição e nivel de ensino
        /// </summary>
        /// <param name="filtros">Filtros para pesquisa de instituição e nivel de ensino</param>
        /// <returns>Lista de tipos de trabalho da instituição x nivel de ensino</returns>
        List<SMCDatasourceItem> BuscarTiposTrabalhoInstituicaoNivelEnsinoSelect(InstituicaoNivelTipoTrabalhoFiltroData filtros);

        /// <summary>
        /// Busca os tipos de pesquisa para trabalhos acadêmicos do BDP ordenados pela prioridade de cada tipo
        /// </summary>
        /// <returns>Lista com os tipos de pesquisa</returns>
        List<TipoPesquisaTrabalhoAcademico> BuscarTipoPesquisaTrabalhoAcademicoOrder();

		bool BuscarGeraFinanceiroEntregaTrabalho(long seqInstituicaoEnsino, long seqTipoTrabalho, long seqNivelEnsino);

	}
}
