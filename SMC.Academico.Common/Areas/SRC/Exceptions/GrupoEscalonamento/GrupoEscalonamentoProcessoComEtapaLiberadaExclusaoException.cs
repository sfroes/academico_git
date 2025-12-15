using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class GrupoEscalonamentoProcessoComEtapaLiberadaExclusaoException : SMCApplicationException
    {
        public GrupoEscalonamentoProcessoComEtapaLiberadaExclusaoException()
            : base(ExceptionsResource.ERR_GrupoEscalonamentoProcessoComEtapaLiberadaExclusao)
        {
        }
    }
}