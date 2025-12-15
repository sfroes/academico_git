using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class GrupoEscalonamentoItemVencimentoDeveSerMaiorDataFimEscalonamentoException : SMCApplicationException
    {
        public GrupoEscalonamentoItemVencimentoDeveSerMaiorDataFimEscalonamentoException()
            : base(ExceptionsResource.ERR_GrupoEscalonamentoItemVencimentoDeveSerMaiorDataFimEscalonamentoException)
        {
        }
    }
}