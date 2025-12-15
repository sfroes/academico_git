using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class ComponenteCurricularAlteracaoEntidadesResponsaveisTurmasException : SMCApplicationException
    {
        public ComponenteCurricularAlteracaoEntidadesResponsaveisTurmasException()
            : base(ExceptionsResource.ERR_ComponenteCurricularAlteracaoEntidadesResponsaveisTurmasException)
        {
        }
    }
}