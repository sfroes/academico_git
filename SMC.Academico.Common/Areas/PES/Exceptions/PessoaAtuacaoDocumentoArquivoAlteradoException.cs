using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.PES.Resources;

namespace SMC.Academico.Common.Areas.PES.Exceptions
{
    public class PessoaAtuacaoDocumentoArquivoAlteradoException : SMCApplicationException
    {
        public PessoaAtuacaoDocumentoArquivoAlteradoException()
            : base(ExceptionsResource.ERR_PessoaAtuacaoDocumentoArquivoAlteradoException)
        {     
        }
    }
}
