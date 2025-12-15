using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class GrupoEscalonamentoProcessoComEtapaEncerradaException : SMCApplicationException
    {
        public GrupoEscalonamentoProcessoComEtapaEncerradaException()
            : base(ExceptionsResource.ERR_GrupoEscalonamentoProcessoComEtapaEncerrada)
        {
        }
    }
}