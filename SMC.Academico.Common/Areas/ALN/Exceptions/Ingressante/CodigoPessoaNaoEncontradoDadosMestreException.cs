using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.ALN.Resources;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class CodigoPessoaNaoEncontradoDadosMestreException : SMCApplicationException
    {
        public CodigoPessoaNaoEncontradoDadosMestreException()
            : base(ExceptionsResource.ERR_CodigoPessoaNaoEncontradoDadosMestreException)
        {
        }
    }
}