using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class GrupoEscalonamentoItemParcelaDataVencimentoMaiorProximaParcelaException : SMCApplicationException
    {
        public GrupoEscalonamentoItemParcelaDataVencimentoMaiorProximaParcelaException()
            : base(ExceptionsResource.ERR_GrupoEscalonamentoItemParcelaDataVencimentoMaiorProximaParcela)
        {
        }
    }
}