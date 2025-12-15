using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CNC.Exceptions.SolicitacaoDocumentoConclusao
{
    public class SolicitacaoDocumentoConclusaoCodigoMigracaoDuplicadoException : SMCApplicationException
    {
        public SolicitacaoDocumentoConclusaoCodigoMigracaoDuplicadoException()
            : base(ExceptionsResource.ERR_SolicitacaoDocumentoConclusaoCodigoMigracaoDuplicadoException)
        { }
    }
}