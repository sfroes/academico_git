using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class ComponenteCurricularAlteracaoDescricaoTurmaDiarioFechadoException : SMCApplicationException
    {
        public ComponenteCurricularAlteracaoDescricaoTurmaDiarioFechadoException()
            : base(ExceptionsResource.ERR_ComponenteCurricularAlteracaoDescricaoTurmaDiarioFechadoException)
        {
        }
    }
}