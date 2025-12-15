using SMC.Academico.Common.Areas.PES.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.PES.Exceptions
{
    public class PessoaEnderecoEletronicoEmailException : SMCApplicationException
    {
        public PessoaEnderecoEletronicoEmailException()
            : base(ExceptionsResource.ERR_PessoaEnderecoEletronicoEmailException)
        {
        }
    }
}