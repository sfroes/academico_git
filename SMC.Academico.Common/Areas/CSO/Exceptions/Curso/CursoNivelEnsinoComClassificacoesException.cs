using SMC.Academico.Common.Areas.CSO.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CSO.Exceptions
{
    public class CursoNivelEnsinoComClassificacoesException : SMCApplicationException
    {
        public CursoNivelEnsinoComClassificacoesException()
            : base(string.Format(ExceptionsResource.ERR_CursoNivelEnsinoComClassificacoesException))
        {
        }
    }
}
