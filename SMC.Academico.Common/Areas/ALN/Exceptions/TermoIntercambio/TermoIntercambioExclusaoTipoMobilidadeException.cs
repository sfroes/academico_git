using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class TermoIntercambioExclusaoTipoMobilidadeException : SMCApplicationException
    {
        public TermoIntercambioExclusaoTipoMobilidadeException(string tipoMobilidade)
            : base(string.Format(Resources.ExceptionsResource.ERR_TermoIntercambioExclusaoTipoMobilidadeException, tipoMobilidade))
        { }
    }
}