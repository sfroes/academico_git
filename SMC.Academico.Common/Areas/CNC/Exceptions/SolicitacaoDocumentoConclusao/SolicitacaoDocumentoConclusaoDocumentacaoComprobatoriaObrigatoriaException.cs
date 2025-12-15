using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CNC.Exceptions.SolicitacaoDocumentoConclusao
{
    public class SolicitacaoDocumentoConclusaoDocumentacaoComprobatoriaObrigatoriaException : SMCApplicationException
    {
        public SolicitacaoDocumentoConclusaoDocumentacaoComprobatoriaObrigatoriaException()
            : base(ExceptionsResource.ERR_SolicitacaoDocumentoConclusaoDocumentacaoComprobatoriaObrigatoriaException)
        { }
    }
}
