using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CNC.Exceptions.SolicitacaoDocumentoConclusao
{
    public class SolicitacaoDocumentoConclusaoTipoIdentidadeNaoInformadaException : SMCApplicationException
    {
        public SolicitacaoDocumentoConclusaoTipoIdentidadeNaoInformadaException()
          : base(ExceptionsResource.ERR_SolicitacaoDocumentoConclusaoTipoIdentidadeNaoInformadaException)
        { }
    }
}
