using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class SolicitacaoAtividadeComplementarTipoPublicacaoObrigatoriaException : SMCApplicationException
    {
        public SolicitacaoAtividadeComplementarTipoPublicacaoObrigatoriaException()
               : base(ExceptionsResource.ERR_SolicitacaoAtividadeComplementarTipoPublicacaoObrigatoriaException)
        { }
    }
}
