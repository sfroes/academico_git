using SMC.Academico.Common.Areas.PES.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.PES.Exceptions
{
    public class PessoaAtuacaoRegistroDocumentoPelaSecretariaException : SMCApplicationException
    {
        public PessoaAtuacaoRegistroDocumentoPelaSecretariaException()
            : base(ExceptionsResource.ERR_PessoaAtuacaoRegistroDocumentoPelaSecretaria)
        {
        }
    }
}