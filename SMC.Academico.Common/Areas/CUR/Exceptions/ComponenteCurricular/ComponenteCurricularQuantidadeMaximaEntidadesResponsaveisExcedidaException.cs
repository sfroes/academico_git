using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class ComponenteCurricularQuantidadeMaximaEntidadesResponsaveisExcedidaException : SMCApplicationException
    {
        public ComponenteCurricularQuantidadeMaximaEntidadesResponsaveisExcedidaException()
            : base(ExceptionsResource.ERR_ComponenteCurricularQuantidadeMaximaEntidadesResponsaveisExcedidaException)
        {
        }
    }
}