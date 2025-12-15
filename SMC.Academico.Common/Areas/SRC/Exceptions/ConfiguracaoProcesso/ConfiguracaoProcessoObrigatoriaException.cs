using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ConfiguracaoProcessoObrigatoriaException : SMCApplicationException
    {
        public ConfiguracaoProcessoObrigatoriaException() 
            : base(ExceptionsResource.ERR_ConfiguracaoProcessoObrigatoria)
        {
        }
    }
}
