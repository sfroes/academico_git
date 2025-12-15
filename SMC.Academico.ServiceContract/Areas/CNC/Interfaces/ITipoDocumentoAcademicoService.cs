using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.ServiceContract.Areas.CNC.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CNC.Interfaces
{
    public interface ITipoDocumentoAcademicoService : ISMCService
    {
        TipoDocumentoAcademicoData BuscarTipoDocumentoAcademico(long seq);

        List<SMCDatasourceItem> BuscarTiposDocumentoAcademicoSelect();

        long SalvarTipoDocumentoAcademico(TipoDocumentoAcademicoData modelo);

        List<SMCDatasourceItem> BuscarTiposDocumentoConclusaoSelect(List<GrupoDocumentoAcademico> gruposDocumentoAcademico, long? seqInstituicaoEnsino);

        List<SMCDatasourceItem> BuscarTiposDocumentoAcademicoPorTipoGrupoDocAcadSelect(GrupoDocumentoAcademico grupoDocumentoAcademico);
    }
}
