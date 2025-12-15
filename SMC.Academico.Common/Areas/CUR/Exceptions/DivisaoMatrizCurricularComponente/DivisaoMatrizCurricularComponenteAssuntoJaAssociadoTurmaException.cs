using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class DivisaoMatrizCurricularComponenteAssuntoJaAssociadoTurmaException : SMCApplicationException
    {
        public DivisaoMatrizCurricularComponenteAssuntoJaAssociadoTurmaException(string curso, string unidade, string turno)
            : base(string.Format(ExceptionsResource.ERR_DivisaoMatrizCurricularComponenteAssuntoJaAssociadoTurmaException, curso, unidade, turno))
        {
        }
    }
}