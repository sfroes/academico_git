using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class EscalonamentoDataFimMenorDataAtualException : SMCApplicationException
    {
        public EscalonamentoDataFimMenorDataAtualException()
            : base(ExceptionsResource.ERR_EscalonamentoDataFimMenorDataAtual)
        {
        }
    }
}