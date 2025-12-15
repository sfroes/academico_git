using SMC.Academico.Common.Areas.PES.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.PES.Exceptions
{
    public class PessoaAtuacaoDuplicadaException : SMCApplicationException
    {
        public PessoaAtuacaoDuplicadaException(string atuacao)
            : base(string.Format(ExceptionsResource.ERR_PessoaAtuacaoDuplicadaException, atuacao))
        {
        }
    }
}