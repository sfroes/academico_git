using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CAM.Exceptions
{
    public class ProcessoSeletivoTipoHierarquiaProcessoGpiDesativadoTipoProcessoUnidadeResponsavelException : SMCApplicationException
    {
        public ProcessoSeletivoTipoHierarquiaProcessoGpiDesativadoTipoProcessoUnidadeResponsavelException(string processoGpi)
            : base(string.Format(Resources.ExceptionsResource.ERR_ProcessoSeletivoTipoHierarquiaProcessoGpiDesativadoTipoProcessoUnidadeResponsavelException, processoGpi))
        { }
    }
}