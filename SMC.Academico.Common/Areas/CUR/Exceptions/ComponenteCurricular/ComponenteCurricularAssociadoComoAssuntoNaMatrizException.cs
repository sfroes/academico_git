using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class ComponenteCurricularAssociadoComoAssuntoNaMatrizException : SMCApplicationException
    {
        public ComponenteCurricularAssociadoComoAssuntoNaMatrizException()
            : base(ExceptionsResource.ERR_ComponenteCurricularAssociadoComoAssuntoNaMatrizException)
        {
        }
    }
}
