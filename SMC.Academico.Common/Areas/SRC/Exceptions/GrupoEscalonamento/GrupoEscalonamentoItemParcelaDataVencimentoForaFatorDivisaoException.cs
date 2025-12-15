using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class GrupoEscalonamentoItemParcelaDataVencimentoForaFatorDivisaoException : SMCApplicationException
    {
        public GrupoEscalonamentoItemParcelaDataVencimentoForaFatorDivisaoException()
            : base(ExceptionsResource.ERR_GrupoEscalonamentoItemParcelaDataVencimentoForaFatorDivisao)
        {
        }
    }
}