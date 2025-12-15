using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class DivisaoMatrizCurricularComponenteJaAssociadaTurmaException : SMCApplicationException
    {
        public DivisaoMatrizCurricularComponenteJaAssociadaTurmaException(string curso, string unidade, string turno)
            : base(string.Format(ExceptionsResource.ERR_DivisaoMatrizCurricularComponenteJaAssociadaTurmaException, curso, unidade, turno))
        {
        }
    }
}