using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class DivisaoMatrizCurricularComponenteAssociadaTurmaException : SMCApplicationException
    {
        public DivisaoMatrizCurricularComponenteAssociadaTurmaException(string curso, string unidade, string turno)
            : base(string.Format(ExceptionsResource.ERR_DivisaoMatrizCurricularComponenteAssociadaTurmaException, curso, unidade, turno))
        {
        }
    }
}
