using SMC.Academico.Domain.Areas.CNC.DomainServices;
using SMC.Academico.ServiceContract.Areas.CNC.Interfaces;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.CNC.Services
{
    public class InstituicaoNivelTipoDocumentoModeloRelatorioService : SMCServiceBase, IInstituicaoNivelTipoDocumentoModeloRelatorioService
    {
        #region [ DomainService ]

        private InstituicaoNivelTipoDocumentoModeloRelatorioDomainService InstituicaoNivelTipoDocumentoModeloRelatorioDomainService => Create<InstituicaoNivelTipoDocumentoModeloRelatorioDomainService>();

        #endregion [ DomainService ]

        public List<SMCDatasourceItem> BuscarIdiomasDocumentoAcademicoSelect(long seqNivelEnsinoPorGrupoDocumentoAcademico, long seqTipoDocumentoAcademico)
        {
            return InstituicaoNivelTipoDocumentoModeloRelatorioDomainService.BuscarIdiomasDocumentoAcademicoSelect(seqNivelEnsinoPorGrupoDocumentoAcademico, seqTipoDocumentoAcademico);
        }
    }
}
