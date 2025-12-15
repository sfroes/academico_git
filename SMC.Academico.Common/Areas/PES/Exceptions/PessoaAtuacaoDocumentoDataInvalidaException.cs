using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.PES.Resources;

namespace SMC.Academico.Common.Areas.PES.Exceptions
{
    public class PessoaAtuacaoDocumentoDataInvalidaException : SMCApplicationException
    {
        public PessoaAtuacaoDocumentoDataInvalidaException()
            : base(ExceptionsResource.ERR_pessoaAtuacaoDocumentoDataInvalidaException)
        {     
        }
    }
}
