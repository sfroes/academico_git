using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class LancamentoNotaBancaExaminadoraNotaObrigatoriaException : SMCApplicationException
    {
        public LancamentoNotaBancaExaminadoraNotaObrigatoriaException()
            : base(ExceptionsResource.ERR_LancamentoNotaBancaExaminadoraNotaObrigatoriaException)
        {
        }
    }
}