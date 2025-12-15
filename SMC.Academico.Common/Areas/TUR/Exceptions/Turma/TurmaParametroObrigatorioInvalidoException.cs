using SMC.Academico.Common.Areas.TUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.TUR.Exceptions
{
    public class TurmaParametroObrigatorioInvalidoException : SMCApplicationException
    {
        public TurmaParametroObrigatorioInvalidoException()
            : base(ExceptionsResource.ERR_TurmaParametroObrigatorioInvalidoException)
        {
        }
    }
}