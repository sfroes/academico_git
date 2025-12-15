using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class ComponenteCurricularSemOrganizacaoException : SMCApplicationException
    {
        public ComponenteCurricularSemOrganizacaoException()
            : base(ExceptionsResource.ERR_ComponenteCurricularSemOrganizacaoExceptionException)
        {
        }
    }
}