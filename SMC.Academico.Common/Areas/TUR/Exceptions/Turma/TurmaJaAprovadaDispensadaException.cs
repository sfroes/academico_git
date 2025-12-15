using SMC.Academico.Common.Areas.TUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.TUR.Exceptions
{
    public class TurmaJaAprovadaDispensadaException : SMCApplicationException
    {
        public TurmaJaAprovadaDispensadaException(string registros, string verbo = "Seleção")
            : base(string.Format(ExceptionsResource.ERR_TurmaJaAprovadaDispensadaException, verbo, registros))
        {
        }
    }
}