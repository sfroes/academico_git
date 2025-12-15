using SMC.Academico.Common.Areas.TUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.TUR.Exceptions
{
    public class TurmaPeriodoLetivoNaoContidoOfertaException : SMCApplicationException
    {
        public TurmaPeriodoLetivoNaoContidoOfertaException()
            : base(ExceptionsResource.ERR_TurmaPeriodoLetivoNaoContidoOfertaException)
        {
        }
    }
}
