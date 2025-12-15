using SMC.Academico.Common.Areas.ALN.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class ConcedeFormacaoExigePeriodoException : SMCApplicationException
    {
        public ConcedeFormacaoExigePeriodoException()
        : base(ExceptionsResource.ERR_ConcedeFormacaoExigePeriodoException)
        {
        }
    }
}
 