using SMC.Academico.Common.Areas.TUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.TUR.Exceptions
{
    public class TurmaComponenteAssuntoObrigatorioException : SMCApplicationException
    {
        public TurmaComponenteAssuntoObrigatorioException()
            : base(ExceptionsResource.ERR_TurmaComponenteAssuntoObrigatorioException)
        {
        }
    }
}
