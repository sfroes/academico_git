using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class ComponenteCurricularObrigatorioEntidadeResponsavelException : SMCApplicationException
    {
        public ComponenteCurricularObrigatorioEntidadeResponsavelException()
            : base(ExceptionsResource.ERR_ComponenteCurricularObrigatorioEntidadeResponsavel)
        {
        }
    }
}