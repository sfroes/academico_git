using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CNC.Exceptions.SolicitacaoDocumentoConclusao
{
    public class SolicitacaoDocumentoConclusaoGraduacaoSemDiplomaDigitalException : SMCApplicationException
    {
        public SolicitacaoDocumentoConclusaoGraduacaoSemDiplomaDigitalException()
            : base(ExceptionsResource.ERR_SolicitacaoDocumentoConclusaoGraduacaoSemDiplomaDigitalException)
        { }
    }
}