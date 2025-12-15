using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class LancamentoNotaBancaExaminadoraMembroComParticipacaoObrigadorioException : SMCApplicationException
    {
        public LancamentoNotaBancaExaminadoraMembroComParticipacaoObrigadorioException()
            : base(ExceptionsResource.ERR_LancamentoNotaBancaExaminadoraMembroComParticipacaoObrigadorioException)
        {
        }
    }
}