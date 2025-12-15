using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;
using SMC.Framework.Util;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class SolicitacaoIntercambioParticipanteExternoFaltanteException : SMCApplicationException
    {
        public SolicitacaoIntercambioParticipanteExternoFaltanteException(TipoParticipacaoOrientacao tipo)
            : base(string.Format(ExceptionsResource.ERR_SolicitacaoIntercambioParticipanteExternoFaltanteException, SMCEnumHelper.GetDescription(tipo)))
        { }
    }
}