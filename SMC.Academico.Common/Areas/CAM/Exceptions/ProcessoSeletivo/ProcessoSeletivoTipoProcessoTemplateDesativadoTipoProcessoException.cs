using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CAM.Exceptions
{
    public class ProcessoSeletivoTipoProcessoTemplateDesativadoTipoProcessoException : SMCApplicationException
    {
        public ProcessoSeletivoTipoProcessoTemplateDesativadoTipoProcessoException(string processoGpi)
            : base(string.Format(Resources.ExceptionsResource.ERR_ProcessoSeletivoTemplateProcessoDesativadoTipoProcessoException, processoGpi))
        { }
    }
}