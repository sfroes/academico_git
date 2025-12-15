using SMC.Academico.Common.Areas.APR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.APR.Exceptions
{
    public class AvaliacaoAlterarFormatoEntregaException : SMCApplicationException
    {
        public AvaliacaoAlterarFormatoEntregaException()
            : base(ExceptionsResource.ERR_AvaliacaoAlterarFormatoEntregaException)
        {
        }
    }
}