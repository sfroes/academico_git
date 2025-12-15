using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;
using SMC.Framework.Util;
using System;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class SolicitacaoIntercambioTipoParticipanteFaltanteException : SMCApplicationException
    {
        public SolicitacaoIntercambioTipoParticipanteFaltanteException(string tipos)
            : base(string.Format(ExceptionsResource.ERR_SolicitacaoIntercambioTipoParticipanteFaltanteException, tipos))
        { }
    }
}