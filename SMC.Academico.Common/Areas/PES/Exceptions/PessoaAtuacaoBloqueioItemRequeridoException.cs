using SMC.Academico.Common.Areas.PES.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.PES.Exceptions
{
    public class PessoaAtuacaoBloqueioItemRequeridoException : SMCApplicationException
    {
        public PessoaAtuacaoBloqueioItemRequeridoException()
            : base(ExceptionsResource.ERR_PessoaAtuacaoBloqueioItemRequeridoException)
        {
        }
    }
}