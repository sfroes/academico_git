using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.PES.Resources;

namespace SMC.Academico.Common.Areas.PES.Exceptions
{
    public class PessoaAtuacaoDocumentoProcessoException : SMCApplicationException
    {
        public PessoaAtuacaoDocumentoProcessoException()
            : base(ExceptionsResource.ERR_PessoaAtuacaoDocumentoProcessoException)
        {     
        }
    }
}
