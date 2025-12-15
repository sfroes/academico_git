using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CNC.Exceptions.SolicitacaoDocumentoConclusao
{
    public class SolicitacaoDocumentoConclusaoConfiguracaoGADNaoEncontradaException : SMCApplicationException
    {
        public SolicitacaoDocumentoConclusaoConfiguracaoGADNaoEncontradaException()
            : base(ExceptionsResource.ERR_SolicitacaoDocumentoConclusaoConfiguracaoGADNaoEncontradaException)
        { }
    }
}