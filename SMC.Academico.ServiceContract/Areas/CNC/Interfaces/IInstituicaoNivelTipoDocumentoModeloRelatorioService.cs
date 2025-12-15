using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;


namespace SMC.Academico.ServiceContract.Areas.CNC.Interfaces
{
    public interface IInstituicaoNivelTipoDocumentoModeloRelatorioService : ISMCService
    {
        List<SMCDatasourceItem> BuscarIdiomasDocumentoAcademicoSelect(long seqNivelEnsinoPorGrupoDocumentoAcademico, long seqTipoDocumentoAcademico);
    }
}
