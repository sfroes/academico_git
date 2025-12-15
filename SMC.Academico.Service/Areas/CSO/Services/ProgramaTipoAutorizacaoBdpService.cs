using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Academico.Domain.Areas.CSO.ValueObjects;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.ServiceContract.Areas.CSO.Data;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.CSO.Services
{
    public class ProgramaTipoAutorizacaoBdpService : SMCServiceBase, IProgramaTipoAutorizacaoBdpService
    {

        #region DomainService

        private ProgramaTipoAutorizacaoBdpDomainService ProgramaTipoAutorizacaoBdpDomainService { get => Create<ProgramaTipoAutorizacaoBdpDomainService>(); }

        #endregion

        /// <summary>
        /// Buscar os tipos de autorização da publicação bpd por programa
        /// </summary>
        /// <param name="seqPrograma">Sequencial do programa</param>
        /// <returns>Lista select dos tipos de autorização</returns>
        public List<SMCDatasourceItem> BuscarTipoAutorizacaoPorProgramaSelect(long seqPrograma)
        {
            return this.ProgramaTipoAutorizacaoBdpDomainService.BuscarTipoAutorizacaoPorProgramaSelect(seqPrograma);
        }
    }
}