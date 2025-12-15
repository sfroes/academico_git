using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CNC.Exceptions.SolicitacaoDocumentoConclusao
{
    public class SolicitacaoDocumentoConclusaoDadosPessoaisDiferente2Exception : SMCApplicationException
    {
        public SolicitacaoDocumentoConclusaoDadosPessoaisDiferente2Exception()
       : base(ExceptionsResource.ERR_SolicitacaoDocumentoConclusaoDadosPessoaisDiferente2Exception)
        { }
    }
}
