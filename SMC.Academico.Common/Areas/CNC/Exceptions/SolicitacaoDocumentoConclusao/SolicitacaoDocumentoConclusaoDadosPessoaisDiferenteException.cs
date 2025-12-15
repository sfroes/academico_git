using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CNC.Exceptions.SolicitacaoDocumentoConclusao
{
    public class SolicitacaoDocumentoConclusaoDadosPessoaisDiferenteException : SMCApplicationException
    {
        public SolicitacaoDocumentoConclusaoDadosPessoaisDiferenteException()
       : base(ExceptionsResource.ERR_SolicitacaoDocumentoConclusaoDadosPessoaisDiferenteException)
        { }
    }
}
