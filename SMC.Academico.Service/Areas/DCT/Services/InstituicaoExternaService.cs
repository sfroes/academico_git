using SMC.Academico.Domain.Areas.DCT.DomainServices;
using SMC.Academico.Domain.Areas.DCT.Specifications;
using SMC.Academico.Domain.Areas.DCT.ValueObjects;
using SMC.Academico.ServiceContract.Areas.DCT.Data;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.DCT.Services
{
    public class InstituicaoExternaService : SMCServiceBase, IInstituicaoExternaService
    {
        #region [ Services ]

        private InstituicaoExternaDomainService InstituicaoExternaDomainService
        {
            get { return this.Create<InstituicaoExternaDomainService>(); }
        }

        #endregion [ Services ]

        /// <summary>
        /// Busca as instituições externas que atendam aos filtros informados
        /// </summary>
        /// <param name="filtros">Filtros das instituições externas ou sequenciais selecionados</param>
        /// <returns>Dados das instituições externas paginados</returns>
        public SMCPagerData<InstituicaoExternaListaData> BuscarInstituicoesExternas(InstituicaoExternaFiltroData filtros)
        {
            return InstituicaoExternaDomainService.BuscarInstituicoesExternas(filtros.Transform<InstituicaoExternaFilterSpecification>()).Transform<SMCPagerData<InstituicaoExternaListaData>>();
        }

        public List<SMCDatasourceItem> BuscarInstituicaoExternaPorColaboradorSelect(InstituicaoExternaFiltroData filtros)
        {
            return InstituicaoExternaDomainService.BuscarInstituicaoExternaPorColaboradorSelect(filtros.Transform<InstituicaoExternaFiltroVO>());
        }
    }
}