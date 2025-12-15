using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CAM.Exceptions
{
    public class ProcessoSeletivoTipoProcessoGpiNaoVinculadoTipoProcessoException : SMCApplicationException
    {
        public ProcessoSeletivoTipoProcessoGpiNaoVinculadoTipoProcessoException(string processoSeletivo)
            : base(string.Format(Resources.ExceptionsResource.ERR_ProcessoSeletivoTipoProcessoGpiNaoVinculadoTipoProcessoException, processoSeletivo))
        { }
    }
}