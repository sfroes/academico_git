using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Framework.Service;

namespace SMC.Academico.ServiceContract.Areas.CNC.Interfaces
{
    public interface IDocumentoAcademicoService : ISMCService
    {
        GrupoDocumentoAcademico? BuscarGrupoDocumentoAcademico(long seqDocumentoAcademicoGAD);
    }
}
