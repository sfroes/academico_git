using SMC.Academico.Common.Areas.PES.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.PES.Exceptions
{
    public class PessoaAtuacaoRegistroDocumentoPeloCRAException : SMCApplicationException
    {
        public PessoaAtuacaoRegistroDocumentoPeloCRAException()
            : base(ExceptionsResource.ERR_PessoaAtuacaoRegistroDocumentoPeloCRA)
        {
        }
    }
}