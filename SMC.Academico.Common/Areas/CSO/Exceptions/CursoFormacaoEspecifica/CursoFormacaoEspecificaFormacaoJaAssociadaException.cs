using SMC.Academico.Common.Areas.CSO.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CSO.Exceptions
{
    public class CursoFormacaoEspecificaFormacaoJaAssociadaException : SMCApplicationException
    {
        public CursoFormacaoEspecificaFormacaoJaAssociadaException()
            : base(ExceptionsResource.ERR_CursoFormacaoEspecificaFormacaoJaAssociadaException)
        {
        }
    }
}
