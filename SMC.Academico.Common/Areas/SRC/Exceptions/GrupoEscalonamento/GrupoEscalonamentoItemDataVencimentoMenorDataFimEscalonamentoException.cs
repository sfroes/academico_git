using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class GrupoEscalonamentoItemDataVencimentoMenorDataFimEscalonamentoException : SMCApplicationException
    {
        public GrupoEscalonamentoItemDataVencimentoMenorDataFimEscalonamentoException()
            : base(ExceptionsResource.ERR_GrupoEscalonamentoItemDataVencimentoMenorDataFimEscalonamentoException)
        {
        }
    }
}