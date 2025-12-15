using SMC.Academico.Common.Areas.CAM.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CAM.Exceptions
{
    public class ProcessoSeletivoOfertaVinculoIngressanteExcluirException : SMCApplicationException
    {
        public ProcessoSeletivoOfertaVinculoIngressanteExcluirException(string ofertaCampanha, string processoSeletivo)
            : base(string.Format(ExceptionsResource.ERR_ProcessoSeletivoOfertaVinculoIngressanteExcluirException, ofertaCampanha, processoSeletivo))
        { }
    }
}
