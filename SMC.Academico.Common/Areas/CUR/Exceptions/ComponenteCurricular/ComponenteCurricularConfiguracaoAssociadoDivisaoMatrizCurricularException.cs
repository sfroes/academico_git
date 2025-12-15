using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class ComponenteCurricularConfiguracaoAssociadoDivisaoMatrizCurricularException : SMCApplicationException
    {
        public ComponenteCurricularConfiguracaoAssociadoDivisaoMatrizCurricularException()
            : base(ExceptionsResource.ERR_ComponenteCurricularConfiguracaoAssociadoDivisaoMatrizCurricularException)
        {
        }
    }
}