using SMC.Academico.Common.Areas.ALN.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class NaoConcedeFormacaoNaoExigePeriodoException : SMCApplicationException
    {
        public NaoConcedeFormacaoNaoExigePeriodoException()
        : base(ExceptionsResource.ERR_NaoConcedeFormacaoNaoExigePeriodoException)
        {
        }
    }
}