using SMC.Academico.Common.Areas.PES.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.PES.Exceptions
{
    public class PessoaEnderecoEletronicoUrlException : SMCApplicationException
    {
        public PessoaEnderecoEletronicoUrlException()
            : base(ExceptionsResource.ERR_PessoaEnderecoEletronicoUrlException)
        {
        }
    }
}