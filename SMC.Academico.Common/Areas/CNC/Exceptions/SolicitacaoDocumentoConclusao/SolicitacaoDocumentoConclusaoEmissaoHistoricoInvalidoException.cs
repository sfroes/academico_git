using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CNC.Exceptions.SolicitacaoDocumentoConclusao
{
    public class SolicitacaoDocumentoConclusaoEmissaoHistoricoInvalidoException : SMCApplicationException
    {
        public SolicitacaoDocumentoConclusaoEmissaoHistoricoInvalidoException()
               : base(ExceptionsResource.ERR_SolicitacaoDocumentoConclusaoEmissaoHistoricoInvalidoException)
        { }
    }
}
