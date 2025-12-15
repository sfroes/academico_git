using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework.Service;

namespace SMC.Academico.Service.Areas.CUR.Services
{
    public class DocumentoAcademicoCurriculoService : SMCServiceBase, IDocumentoAcademicoCurriculoService
    {
        private DocumentoAcademicoCurriculoDomainService DocumentoAcademicoCurriculoDomainService => Create<DocumentoAcademicoCurriculoDomainService>();

        public void AtualizarDocumentoAcademicoCurriculoDigital(long seqDocumentoAcademicoGAD)
        {
            DocumentoAcademicoCurriculoDomainService.AtualizarDocumentoAcademicoCurriculoDigital(seqDocumentoAcademicoGAD);
        }

    }
}
