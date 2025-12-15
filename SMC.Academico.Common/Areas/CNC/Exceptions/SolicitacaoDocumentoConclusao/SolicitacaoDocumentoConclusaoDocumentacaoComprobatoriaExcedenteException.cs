using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CNC.Exceptions.SolicitacaoDocumentoConclusao
{
    public class SolicitacaoDocumentoConclusaoDocumentacaoComprobatoriaExcedenteException : SMCApplicationException
    {
        public SolicitacaoDocumentoConclusaoDocumentacaoComprobatoriaExcedenteException(string documento)
            : base(string.Format(ExceptionsResource.ERR_SolicitacaoDocumentoConclusaoDocumentacaoComprobatoriaExcedenteException, documento))
        { }
    }
}
