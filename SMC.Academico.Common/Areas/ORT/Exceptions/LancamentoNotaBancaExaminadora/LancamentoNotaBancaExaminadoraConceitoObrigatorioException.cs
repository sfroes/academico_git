using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class LancamentoNotaBancaExaminadoraConceitoObrigatorioException : SMCApplicationException
    {
        public LancamentoNotaBancaExaminadoraConceitoObrigatorioException()
            : base(ExceptionsResource.ERR_LancamentoNotaBancaExaminadoraConceitoObrigatorioException)
        {
        }
    }
}