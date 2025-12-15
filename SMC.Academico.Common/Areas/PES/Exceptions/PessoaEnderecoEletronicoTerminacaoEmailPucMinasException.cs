using SMC.Academico.Common.Areas.PES.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.PES.Exceptions
{
    public class PessoaEnderecoEletronicoTerminacaoEmailPucMinasException : SMCApplicationException
    {
        public PessoaEnderecoEletronicoTerminacaoEmailPucMinasException()
            : base(ExceptionsResource.ERR_PessoaEnderecoEletronicoTerminacaoEmailPucMinasException)
        {
        }
    }
}