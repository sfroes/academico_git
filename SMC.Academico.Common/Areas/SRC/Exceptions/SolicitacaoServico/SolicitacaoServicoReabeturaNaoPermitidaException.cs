using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions.SolicitacaoServico
{
    public class SolicitacaoServicoReabeturaNaoPermitidaException : SMCApplicationException
    {
        public SolicitacaoServicoReabeturaNaoPermitidaException()
           : base(ExceptionsResource.ERR_SolicitacaoServicoReabeturaNaoPermitidaException)
        { }
    }
}
