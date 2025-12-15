using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.GRD.Resources;

namespace SMC.Academico.Common.Areas.GRD.Exceptions
{
    public class ConfiguracaoGradeAcoesNaoPermitidasEventoAulaException : SMCApplicationException
    {
        public ConfiguracaoGradeAcoesNaoPermitidasEventoAulaException() :
           base(ExceptionsResource.ERR_ConfiguracaoGradeAcoesNaoPermitidasEventoAulaException)
        {
        }
    }
}
