using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.PES.Resources;
using SMC.Framework.Util;

namespace SMC.Academico.Common.Areas.PES.Exceptions
{
    public class PessoaAtuacaoDocumentoDocumentoRequeridoException : SMCApplicationException
    {
        public PessoaAtuacaoDocumentoDocumentoRequeridoException()
            : base(ExceptionsResource.ERR_PessoaAtuacaoDocumentoDocumentoRequeridoException)
        {     
        }
    }
}
