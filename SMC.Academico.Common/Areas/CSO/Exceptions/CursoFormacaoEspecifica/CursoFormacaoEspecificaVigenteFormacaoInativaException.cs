using SMC.Academico.Common.Areas.CSO.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CSO.Exceptions
{
    public class CursoFormacaoEspecificaVigenteFormacaoInativaException : SMCApplicationException
    {
        public CursoFormacaoEspecificaVigenteFormacaoInativaException()
            : base(ExceptionsResource.ERR_CursoFormacaoEspecificaVigenteFormacaoInativaException)
        {
        }
    }
}

