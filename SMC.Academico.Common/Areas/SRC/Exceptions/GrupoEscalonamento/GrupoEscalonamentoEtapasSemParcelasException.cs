using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class GrupoEscalonamentoEtapasSemParcelasException : SMCApplicationException
    {
        public GrupoEscalonamentoEtapasSemParcelasException()
            : base(ExceptionsResource.ERR_GrupoEscalonamentoEtapasSemParcelas)
        {}        
    }
}