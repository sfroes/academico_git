using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.PES.Resources;

namespace SMC.Academico.Common.Areas.PES.Exceptions
{
    public class PessoaAtuacaoDocumentoException : SMCApplicationException
    {
        public PessoaAtuacaoDocumentoException()
            : base(ExceptionsResource.ERR_PessoaAtuacaoDocumentoSemConfiguracaoException)
        {     
        }
    }
}
