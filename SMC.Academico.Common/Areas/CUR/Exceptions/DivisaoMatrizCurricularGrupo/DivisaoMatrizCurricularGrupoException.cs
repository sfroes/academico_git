using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class DivisaoMatrizCurricularGrupoException : SMCApplicationException
    {
        public DivisaoMatrizCurricularGrupoException()
            : base(ExceptionsResource.ERR_DivisaoMatrizCurricularGrupoQuantidade)
        {
        }
    }
}