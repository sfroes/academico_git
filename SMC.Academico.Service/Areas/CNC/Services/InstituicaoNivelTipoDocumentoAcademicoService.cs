using SMC.Academico.Common.Areas.CNC.Enums;
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
    public class InstituicaoNivelTipoDocumentoAcademicoService : SMCServiceBase, IInstituicaoNivelTipoDocumentoAcademicoService
    {
        #region [ DomainService ]

        private InstituicaoNivelTipoDocumentoAcademicoDomainService InstituicaoNivelTipoDocumentoAcademicoDomainService => Create<InstituicaoNivelTipoDocumentoAcademicoDomainService>();

        #endregion [ DomainService ]

        public InstituicaoNivelTipoDocumentoAcademicoData BuscarInstituicaoNivelTipoDocumentoAcademico(long seq)
        {
            return InstituicaoNivelTipoDocumentoAcademicoDomainService.BuscarInstituicaoNivelTipoDocumentoAcademico(seq).Transform<InstituicaoNivelTipoDocumentoAcademicoData>();
        }

        public long Salvar(InstituicaoNivelTipoDocumentoAcademicoData modelo)
        {
            return InstituicaoNivelTipoDocumentoAcademicoDomainService.SalvarInstituicaoNivelTipoDocumentoAcademico(modelo.Transform<InstituicaoNivelTipoDocumentoAcademicoVO>());
        }

        public List<SMCDatasourceItem> BuscarNiveisEnsinoPorGrupoDocumentoAcademicoSelect(GrupoDocumentoAcademico grupoDocumento)
        {
            return InstituicaoNivelTipoDocumentoAcademicoDomainService.BuscarNiveisEnsinoPorGrupoDocumentoAcademicoSelect(grupoDocumento);
        }

        public List<SMCDatasourceItem> BuscarTiposDocumentoAcademicoPorNivelEnsinoSelect(long seqNivelEnsinoPorGrupoDocumentoAcademico, GrupoDocumentoAcademico grupoDocumento)
        {
            return InstituicaoNivelTipoDocumentoAcademicoDomainService.BuscarTiposDocumentoAcademicoPorNivelEnsinoSelect(seqNivelEnsinoPorGrupoDocumentoAcademico, grupoDocumento);
        }

        public bool ValidarTipoDocumentoAcademicoArquivoXml(long seqTipoDocumentoAcademico)
        {
            return InstituicaoNivelTipoDocumentoAcademicoDomainService.ValidarTipoDocumentoAcademicoArquivoXml(seqTipoDocumentoAcademico);
        }

        public void ValidarPermissaoConfigurarRelatorio(long seqNivelEnsinoPorGrupoDocumentoAcademico, long seqTipoDocumentoAcademico)
        {
            InstituicaoNivelTipoDocumentoAcademicoDomainService.ValidarPermissaoConfigurarRelatorio(seqNivelEnsinoPorGrupoDocumentoAcademico, seqTipoDocumentoAcademico);
        }
    }
}
