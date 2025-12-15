using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class SolicitacaoServicoComEtapaAnteriorNaoFinalizadaException : SMCApplicationException
    {
        public SolicitacaoServicoComEtapaAnteriorNaoFinalizadaException()
            : base(ExceptionsResource.ERR_SolicitacaoServicoComEtapaAnteriorNaoFinalizadaException)
        { }
    }
}