using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class ComponenteCurricularAlteracaoEntidadesResponsaveisAssuntoComponentesException : SMCApplicationException
    {
        public ComponenteCurricularAlteracaoEntidadesResponsaveisAssuntoComponentesException()
            : base(ExceptionsResource.ERR_ComponenteCurricularAlteracaoEntidadesResponsaveisAssuntoComponentesException)
        {
        }
    }
}