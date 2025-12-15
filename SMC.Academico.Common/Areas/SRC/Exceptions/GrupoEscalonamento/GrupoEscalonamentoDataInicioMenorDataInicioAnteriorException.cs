using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class GrupoEscalonamentoDataInicioMenorDataInicioAnteriorException : SMCApplicationException
    {
        public GrupoEscalonamentoDataInicioMenorDataInicioAnteriorException()
            : base(ExceptionsResource.ERR_GrupoEscalonamentoDataInicioMenorDataInicioAnterior)
        {
        }
    }
}