using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;
using SMC.Framework.Util;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class SolicitacaoIntercambioParticipanteInternoFaltanteException : SMCApplicationException
    {
        public SolicitacaoIntercambioParticipanteInternoFaltanteException(TipoParticipacaoOrientacao tipo)
            : base(string.Format(ExceptionsResource.ERR_SolicitacaoIntercambioParticipanteInternoFaltanteException, SMCEnumHelper.GetDescription(tipo)))
        { }
    }
}