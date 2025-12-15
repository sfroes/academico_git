using SMC.Academico.Common.Areas.ALN.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class ExigeOfertaMatrizCurricularExigeCursoException : SMCApplicationException
    {
        public ExigeOfertaMatrizCurricularExigeCursoException()
        : base(ExceptionsResource.ERR_ExigeOfertaMatrizCurricularExigeCursoException)
        {
        }
    }
}

 