using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;
using SMC.Framework.Util;
using System.Collections.Generic;

namespace SMC.Academico.Common.Areas.ORT.Exceptions.PublicacaoBdp
{
    public class PublicacaoBdpSolicitacaoAbertaAbortarException : SMCApplicationException
    {
        public PublicacaoBdpSolicitacaoAbertaAbortarException(List<string> descricaoProcessos)
            : base(string.Format(ExceptionsResource.ERR_PublicacaoBdpSolicitacaoAbertaAbortarException, SMCStringHelper.JoinWithLastSeparatorIgnoringNullOrEmpty(", ", " e ", descricaoProcessos)))
        {
        }
    }
}