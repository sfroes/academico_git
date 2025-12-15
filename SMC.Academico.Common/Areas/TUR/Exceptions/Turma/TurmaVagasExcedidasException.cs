using SMC.Academico.Common.Areas.TUR.Resources;
using SMC.Framework.Exceptions;


namespace SMC.Academico.Common.Areas.TUR.Exceptions
{
    public class TurmaVagasExcedidasException : SMCApplicationException
    {
        public TurmaVagasExcedidasException(string texto)
            : base(string.Format(ExceptionsResource.ERR_TurmaVagasExcedidasException, texto))
        {
        }
    }
}
