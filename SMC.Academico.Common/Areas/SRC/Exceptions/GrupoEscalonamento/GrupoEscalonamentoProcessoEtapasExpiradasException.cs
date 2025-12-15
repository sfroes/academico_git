using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class GrupoEscalonamentoProcessoEtapasExpiradasException : SMCApplicationException
    {
        public GrupoEscalonamentoProcessoEtapasExpiradasException(string status)
            : base(string.Format(ExceptionsResource.ERR_GrupoEscalonamentoProcessoEtapasExpiradasException, status))
        {
        }
    }
}