using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CNC.Exceptions.SolicitacaoDocumentoConclusao
{
    public class SolicitacaoDocumentoConclusaoDataColacaoMenorException : SMCApplicationException
    {
        public SolicitacaoDocumentoConclusaoDataColacaoMenorException()
            : base(ExceptionsResource.ERR_SolicitacaoDocumentoConclusaoDataColacaoMenorException)
        { }
    }
}