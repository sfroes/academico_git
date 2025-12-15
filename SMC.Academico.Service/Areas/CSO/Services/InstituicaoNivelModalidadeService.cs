using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.CSO.Services
{
    public class InstituicaoNivelModalidadeService : SMCServiceBase, IInstituicaoNivelModalidadeService
    {
        #region [ DomainService ]

        private InstituicaoNivelModalidadeDomainService InstituicaoNivelModalidadeDomainService
        {
            get { return this.Create<InstituicaoNivelModalidadeDomainService>(); }
        }

        #endregion [ DomainService ]

        /// <summary>
        /// Busca modalidades para a listagem de acordo com o instituição nível ensino
        /// </summary>
        /// <param name="seqInstituicaoNivelEnsino">Sequencial da instituição nível ensino</param>
        /// <returns>Lista de modalidades</returns>
        public List<SMCDatasourceItem> BuscarModalidadesPorInstituicaoNivelEnsinoSelect(long seqInstituicaoNivelEnsino)
        {
            return InstituicaoNivelModalidadeDomainService.BuscarModalidadesPorNivelEnsinoSelect(seqInstituicaoNivelEnsino: seqInstituicaoNivelEnsino);
        }

        /// <summary>
        /// Busca modalidades para a listagem de acordo com o instituição nível ensino
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial da instituição nível ensino</param>
        /// <returns>Lista de modalidades</returns>
        public List<SMCDatasourceItem> BuscarModalidadesPorNivelEnsinoSelect(long seqNivelEnsino)
        {
            return InstituicaoNivelModalidadeDomainService.BuscarModalidadesPorNivelEnsinoSelect(seqNivelEnsino: seqNivelEnsino);
        }

        /// <summary>
        /// Busca modalidades para a listagem de acordo com o instituição
        /// </summary>
        /// <returns>Lista de modalidades</returns>
        public List<SMCDatasourceItem> BuscarModalidadesPorInstituicaoSelect()
        {
            return InstituicaoNivelModalidadeDomainService.BuscarModalidadesPorInstituicaoSelect();
        }

        /// <summary>
        /// Busca modalidades para a listagem de acordo com o instituição logada
        /// </summary>
        /// <param name="seqInstituicao">Sequencial da instituição</param>
        /// <returns>Lista de modalidades</returns>
        public List<SMCDatasourceItem> BuscarModalidadesPorInstituicaoLogadaSelect(long seqInstituicao)
        {
            return InstituicaoNivelModalidadeDomainService.BuscarModalidadesPorInstituicaoLogadaSelect(seqInstituicao);
        }
    }
}