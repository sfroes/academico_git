using SMC.Academico.Domain.Areas.DCT.DomainServices;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.DCT.Services
{
    public class CategoriaInstituicaoEnsinoService : SMCServiceBase, ICategoriaInstituicaoEnsinoService
    {
        #region [ Services ]

        private CategoriaInstituicaoEnsinoDomainService CategoriaInstituicaoEnsinoDomainService
        {
            get { return this.Create<CategoriaInstituicaoEnsinoDomainService>(); }
        }

        #endregion [ Services ]

        /// <summary>
        /// Busca os dados de todas as categorias de instituição de ensino cadastradas
        /// </summary>
        /// <returns>Lista de SMCDatasourceItem com o sequencial e descrição das categorias</returns>
        public List<SMCDatasourceItem> BuscarCategoriasInstituicaoEnsinoSelect()
        {
            return this.CategoriaInstituicaoEnsinoDomainService.BuscarCategoriasInstituicaoEnsinoSelect();
        }
    }
}