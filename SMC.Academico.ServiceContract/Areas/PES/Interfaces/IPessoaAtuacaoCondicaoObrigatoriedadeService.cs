using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Framework.Service;

namespace SMC.Academico.ServiceContract.Areas.PES.Interfaces
{
    public interface IPessoaAtuacaoCondicaoObrigatoriedadeService : ISMCService
    {
        PessoaAtuacaoCondicaoObrigatoriedadeData AlterarPessoaAtuacaoCondicaoObrigatoriedade(long seqPessoaAtuacao);

        long SalvarPessoaAtuacaoCondicaoObrigatoriedade(PessoaAtuacaoCondicaoObrigatoriedadeData obj);
    }
}