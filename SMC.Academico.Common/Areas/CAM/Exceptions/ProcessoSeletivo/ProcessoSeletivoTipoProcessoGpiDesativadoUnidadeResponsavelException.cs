using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CAM.Exceptions
{
    public class ProcessoSeletivoTipoProcessoGpiDesativadoUnidadeResponsavelException : SMCApplicationException
    {
        public ProcessoSeletivoTipoProcessoGpiDesativadoUnidadeResponsavelException(string processoGpi)
            : base(string.Format(Resources.ExceptionsResource.ERR_ProcessoSeletivoTipoProcessoGpiDesativadoUnidadeResponsavelException, processoGpi))
        { }
    }
}