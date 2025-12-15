using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CNC.Exceptions.SolicitacaoDocumentoConclusao
{
    public class SolicitacaoDocumentoConclusaoNovaViaException : SMCApplicationException
    {
        public SolicitacaoDocumentoConclusaoNovaViaException()
            : base(ExceptionsResource.ERR_SolicitacaoDocumentoConclusaoNovaViaException)
        { }
    }
}