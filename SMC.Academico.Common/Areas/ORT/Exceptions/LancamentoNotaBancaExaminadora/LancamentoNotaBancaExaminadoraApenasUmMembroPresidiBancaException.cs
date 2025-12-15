using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class LancamentoNotaBancaExaminadoraApenasUmMembroPresidiBancaException : SMCApplicationException
    {
        public LancamentoNotaBancaExaminadoraApenasUmMembroPresidiBancaException()
            : base(ExceptionsResource.ERR_LancamentoNotaBancaExaminadoraApenasUmMembroPresidirBancaException)
        {
        }
    }
}