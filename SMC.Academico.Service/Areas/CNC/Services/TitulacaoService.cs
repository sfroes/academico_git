using SMC.Academico.Domain.Areas.CNC.DomainServices;
using SMC.Academico.Domain.Areas.CNC.ValueObjects;
using SMC.Academico.ServiceContract.Areas.CNC.Data;
using SMC.Academico.ServiceContract.Areas.CNC.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.CNC.Services
{
    public class TitulacaoService : SMCServiceBase, ITitulacaoService
    {
        #region [ Service ]

        private TitulacaoDomainService TitulacaoDomainService
        {
            get { return this.Create<TitulacaoDomainService>(); }
        }

        #endregion [ Service ]

        /// <summary>
        /// Busca uma titulação
        /// </summary>
        /// <param name="seq">Sequencia da titulação a ser recuperada</param>
        /// <returns>Dados da titulação</returns>
        public TitulacaoData BuscarTitulacao(long seq)
        {
            return TitulacaoDomainService.BuscarTitulacao(seq).Transform<TitulacaoData>();
        }

        /// <summary>
        /// Busca as titulações que atendam os filtros informados
        /// </summary>
        /// <param name="filtro">Filtros da listagem de titulação</param>
        /// <returns>Lista de titulações</returns>
        public SMCPagerData<TitulacaoData> BuscarTitulacoes(TitulacaoFiltroData filtro)
        {
            //filtro.PageSettings.SortInfo.Clear();
            return TitulacaoDomainService.BuscarTitulacoes(filtro.Transform<TitulacaoFiltroVO>()).Transform<SMCPagerData<TitulacaoData>>();
        }

        /// <summary>
        /// Busca as titulações que atendam os filtros informados
        /// </summary>
        /// <param name="filtros">Dados do filtro</param>
        /// <returns>Lista de titulações para select.
        /// A descrição será apresentada conforme o flag DescricaoAbreviada, sendo por padrão completa.
        /// Caso o campo sexo seja informado, será retornada apenas a descrição do genero informado.</returns>
        public List<SMCDatasourceItem> BuscarTitulacoesSelect(TitulacaoFiltroData filtros)
        {
            return TitulacaoDomainService.BuscarTitulacoesSelect(filtros.Transform<TitulacaoFiltroVO>());
        }

        /// <summary>
        /// Grava uma titulação
        /// </summary>
        /// <param name="titulacao">Titulação a ser gravado</param>
        /// <returns>Sequencia da titulação gravada</returns>
        public long SalvarTitulacao(TitulacaoData titulacao)
        {
            return TitulacaoDomainService.SalvarTitulacao(titulacao.Transform<TitulacaoVO>());
        }
    }
}