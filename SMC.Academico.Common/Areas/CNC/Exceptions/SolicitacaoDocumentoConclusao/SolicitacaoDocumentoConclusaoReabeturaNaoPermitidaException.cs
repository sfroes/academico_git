using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;


namespace SMC.Academico.Common.Areas.CNC.Exceptions.SolicitacaoDocumentoConclusao
{
    public class SolicitacaoDocumentoConclusaoReabeturaNaoPermitidaException : SMCApplicationException
    {
        public SolicitacaoDocumentoConclusaoReabeturaNaoPermitidaException()
            : base(ExceptionsResource.ERR_SolicitacaoDocumentoConclusaoReabeturaNaoPermitidaException)
        { }
    }
}
