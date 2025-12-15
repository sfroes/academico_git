using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Domain.Areas.CNC.DomainServices;
using SMC.Academico.ServiceContract.Areas.CNC.Interfaces;
using SMC.Framework.Service;

namespace SMC.Academico.Service.Areas.CNC.Services
{
    public class DocumentoAcademicoService : SMCServiceBase, IDocumentoAcademicoService
    {
        private DocumentoAcademicoDomainService DocumentoAcademicoDomainService => Create<DocumentoAcademicoDomainService>();

        public GrupoDocumentoAcademico? BuscarGrupoDocumentoAcademico(long seqDocumentoAcademicoGAD)
        {
            return DocumentoAcademicoDomainService.BuscarGrupoDocumentoAcademico(seqDocumentoAcademicoGAD);
        }
    }
}
