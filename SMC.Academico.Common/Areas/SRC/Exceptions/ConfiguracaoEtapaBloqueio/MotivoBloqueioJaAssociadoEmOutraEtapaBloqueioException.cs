using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;


namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class MotivoBloqueioJaAssociadoEmOutraEtapaBloqueioException : SMCApplicationException
    {
        public MotivoBloqueioJaAssociadoEmOutraEtapaBloqueioException()
            : base(ExceptionsResource.ERR_MotivoBloqueioJaAssociadoEmOutraEtapaBloqueioException)
        {
        }
    }
}
