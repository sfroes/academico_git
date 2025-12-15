using SMC.Academico.Common.Areas.FIN.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.FIN.Exceptions
{
    public class PessoaAtuacaoBeneficioAlunoSituacaoException : SMCApplicationException
    {
        public PessoaAtuacaoBeneficioAlunoSituacaoException(string status)
            : base(string.Format(ExceptionsResource.ERR_PessoaAtuacaoBeneficioAlunoSituacaoException, status))
        { }
    }
}