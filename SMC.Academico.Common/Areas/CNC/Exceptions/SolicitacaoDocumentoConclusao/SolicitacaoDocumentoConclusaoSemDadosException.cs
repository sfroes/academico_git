using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;


namespace SMC.Academico.Common.Areas.CNC.Exceptions.SolicitacaoDocumentoConclusao
{
    public class SolicitacaoDocumentoConclusaoSemDadosException : SMCApplicationException
    {
        public SolicitacaoDocumentoConclusaoSemDadosException()
            : base(ExceptionsResource.ERR_SolicitacaoDocumentoConclusaoSemDadosException)
        { }
    }
}
