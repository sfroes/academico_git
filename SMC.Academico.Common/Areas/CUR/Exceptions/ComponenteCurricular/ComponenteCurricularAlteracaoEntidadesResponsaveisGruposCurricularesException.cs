using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class ComponenteCurricularAlteracaoEntidadesResponsaveisGruposCurricularesException : SMCApplicationException
    {
        public ComponenteCurricularAlteracaoEntidadesResponsaveisGruposCurricularesException()
            : base(ExceptionsResource.ERR_ComponenteCurricularAlteracaoEntidadesResponsaveisGruposCurricularesException)
        {
        }
    }
}