using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.ServiceContract.Areas.CNC.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CNC.Interfaces
{
    public interface IInstituicaoNivelTipoDocumentoAcademicoService : ISMCService
    {
        InstituicaoNivelTipoDocumentoAcademicoData BuscarInstituicaoNivelTipoDocumentoAcademico(long seq);

        long Salvar(InstituicaoNivelTipoDocumentoAcademicoData modelo);

        List<SMCDatasourceItem> BuscarNiveisEnsinoPorGrupoDocumentoAcademicoSelect(GrupoDocumentoAcademico grupoDocumento);

        List<SMCDatasourceItem> BuscarTiposDocumentoAcademicoPorNivelEnsinoSelect(long seqNivelEnsinoPorGrupoDocumentoAcademico, GrupoDocumentoAcademico grupoDocumento);

        bool ValidarTipoDocumentoAcademicoArquivoXml(long seqTipoDocumentoAcademico);

        void ValidarPermissaoConfigurarRelatorio(long seqNivelEnsinoPorGrupoDocumentoAcademico, long seqTipoDocumentoAcademico);
    }
}
