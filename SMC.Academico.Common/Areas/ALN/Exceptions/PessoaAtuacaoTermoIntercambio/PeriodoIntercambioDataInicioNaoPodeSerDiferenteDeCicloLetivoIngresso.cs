using SMC.Academico.Common.Areas.ALN.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class PeriodoIntercambioDataInicioNaoPodeSerDiferenteDeCicloLetivoIngresso : SMCApplicationException
    {
        public PeriodoIntercambioDataInicioNaoPodeSerDiferenteDeCicloLetivoIngresso(string anoNumeroCicloLetivo) 
            : base(string.Format(ExceptionsResource.ERR_PeriodoIntercambioDataInicioNaoPodeSerDIferenteDeCicloLetivoIngresso, anoNumeroCicloLetivo))
        {
        }
    }
}
