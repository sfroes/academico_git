using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ProcessoEtapaDataFimEscalonamentosMaiorEscalonamentoEtapaPosteriorException : SMCApplicationException
    {
        public ProcessoEtapaDataFimEscalonamentosMaiorEscalonamentoEtapaPosteriorException()
            : base(ExceptionsResource.ERR_ProcessoEtapaDataFimEscalonamentosMaiorEscalonamentoEtapaPosterior)
        {
        }
    }
}