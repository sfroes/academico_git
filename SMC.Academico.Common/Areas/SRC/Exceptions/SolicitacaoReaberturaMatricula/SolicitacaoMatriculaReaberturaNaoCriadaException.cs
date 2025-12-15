using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class SolicitacaoMatriculaReaberturaNaoCriadaException : SMCApplicationException
    {
        public SolicitacaoMatriculaReaberturaNaoCriadaException()
            : base(ExceptionsResource.ERR_SolicitacaoMatriculaReaberturaNaoCriadaException)
        {
        }
    }
}