using SMC.Academico.Common.Areas.TUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.TUR.Exceptions
{
    public class TurmaOfertasSemParametrosInvalidoException : SMCApplicationException
    {
        public TurmaOfertasSemParametrosInvalidoException(string registros)
            : base(string.Format(ExceptionsResource.ERR_TurmaOfertasSemParametrosInvalidoException, registros))
        {
        }
    }
}