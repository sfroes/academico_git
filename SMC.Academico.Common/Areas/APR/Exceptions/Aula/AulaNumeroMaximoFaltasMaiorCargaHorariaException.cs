using SMC.Academico.Common.Areas.APR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.APR.Exceptions.Aula
{
    public class AulaNumeroMaximoFaltasMaiorCargaHorariaException : SMCApplicationException
    {
        public AulaNumeroMaximoFaltasMaiorCargaHorariaException(short cargaHoraria)
            : base(string.Format(ExceptionsResource.ERR_AulaNumeroMaximoFaltasMaiorCargaHorariaException, cargaHoraria))
        {
        }
    }
}