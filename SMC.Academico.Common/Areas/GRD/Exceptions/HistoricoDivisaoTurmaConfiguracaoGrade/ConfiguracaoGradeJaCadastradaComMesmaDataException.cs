using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.GRD.Resources;

namespace SMC.Academico.Common.Areas.GRD.Exceptions
{
    public class ConfiguracaoGradeJaCadastradaComMesmaDataException : SMCApplicationException
    {
        public ConfiguracaoGradeJaCadastradaComMesmaDataException() :
           base(ExceptionsResource.ERR_ConfiguracaoGradeJaCadastradaComMesmaDataException)
        {
        }
    }
}
