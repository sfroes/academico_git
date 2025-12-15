using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.CAM.Services
{
    public class InstituicaoNivelRegimeLetivoService : SMCServiceBase, IInstituicaoNivelRegimeLetivoService
    {
        #region [ Serviços ]

        private InstituicaoNivelRegimeLetivoDomainService InstituicaoNivelRegimeLetivoDomainService
        {
            get { return this.Create<InstituicaoNivelRegimeLetivoDomainService>(); }
        }

        #endregion

        /// <summary>
        /// Busca a lista de níveis de ensino da instituição de ensino logada no regime informado
        /// </summary>
        /// <param name="seqRegimeLetivo">Sequencial do regime letivo</param>
        /// <returns>Lista de níveis de ensino com sequencial do NivelEnsino</returns>
        public List<SMCDatasourceItem> BuscarNiveisEnsinoDoRegimeSelect(long seqRegimeLetivo)
        {
            return this.InstituicaoNivelRegimeLetivoDomainService.BuscarNiveisEnsinoDoRegimeSelect(seqRegimeLetivo);
        }
    }
}
