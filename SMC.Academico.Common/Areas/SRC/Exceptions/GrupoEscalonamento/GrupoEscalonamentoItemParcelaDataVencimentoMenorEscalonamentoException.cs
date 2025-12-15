using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class GrupoEscalonamentoItemParcelaDataVencimentoMenorEscalonamentoException : SMCApplicationException
    {
        public GrupoEscalonamentoItemParcelaDataVencimentoMenorEscalonamentoException()
            : base(ExceptionsResource.ERR_GrupoEscalonamentoItemParcelaDataVencimentoMenorEscalonamento)
        {
        }
    }
}