using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Framework.Service;

namespace SMC.Academico.ServiceContract.Areas.PES.Interfaces
{
    public interface IFormacaoAcademicaService : ISMCService
    {
        FormacaoAcademicaCabecalhoData BuscarFormacaoAcademicaCabecalho(long seq);

        FormacaoAcademicaInsertedData BuscarFormacaoAcademicaInserted(FormacaoAcademicaInsertedData model);

        FormacaoAcademicaData BuscarFormacaoAcademica(long seq);

        long SalvarFormacaoAcademica(FormacaoAcademicaData model);

        void ExcluirFormacaoAcademica(FormacaoAcademicaData model);

        bool ValidarTitulacaoMaxima(long seqPessoaAtuacao, bool? titulacaoMaxima, long seq);
    }
}
