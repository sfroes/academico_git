using SMC.Framework.Service;

namespace SMC.Academico.ServiceContract.Areas.CUR.Interfaces
{
    public interface IDocumentoAcademicoCurriculoService : ISMCService
    {
        void AtualizarDocumentoAcademicoCurriculoDigital(long seqDocumentoAcademicoGAD);
    }
}
