using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;


namespace SMC.Academico.Common.Areas.CNC.Exceptions.SolicitacaoDocumentoConclusao
{
    public class SolicitacaoDocumentoConclusaoSemGrauAcademicoException : SMCApplicationException
    {
        public SolicitacaoDocumentoConclusaoSemGrauAcademicoException()
            : base(ExceptionsResource.ERR_SolicitacaoDocumentoConclusaoSemGrauAcademicoException)
        { }
    }
}
