using SMC.Academico.Common.Areas.PES.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.PES.Exceptions
{
    public class PessoaAtuacaoBloqueioDesbloqueioJaDesbloqueadoException : SMCApplicationException
    {
        public PessoaAtuacaoBloqueioDesbloqueioJaDesbloqueadoException()
            : base(ExceptionsResource.ERR_PessoaAtuacaoBloqueioDesbloqueioJaDesbloqueadoException)
        {
        }
    }
}