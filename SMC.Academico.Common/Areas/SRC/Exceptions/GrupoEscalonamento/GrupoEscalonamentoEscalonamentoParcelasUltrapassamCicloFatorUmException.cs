using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class GrupoEscalonamentoEscalonamentoParcelasUltrapassamCicloFatorUmException : SMCApplicationException
    {
        public GrupoEscalonamentoEscalonamentoParcelasUltrapassamCicloFatorUmException()
            : base(ExceptionsResource.ERR_GrupoEscalonamentoEscalonamentoParcelasUltrapassamCicloFatorUm)
        {
        }
    }
}