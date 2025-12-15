using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CNC.Exceptions.SolicitacaoDocumentoConclusao
{
    public class SolicitacaoDocumentoConclusaoSemDataExpedicaoDiplomaException : SMCApplicationException
    {
        public SolicitacaoDocumentoConclusaoSemDataExpedicaoDiplomaException()
          : base(ExceptionsResource.ERR_SolicitacaoDocumentoConclusaoSemDataExpedicaoDiplomaException)
        { }
    }
}
