using SMC.Academico.Common.Areas.ALN.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class UrlAvaliacaoCpaNaoConfiguradaException : SMCApplicationException
    {
        public UrlAvaliacaoCpaNaoConfiguradaException(string tipoAvaliacao) 
            : base(string.Format(ExceptionsResource.ERR_UrlAvaliacaoCpaNaoConfiguradaException, tipoAvaliacao))
        { }
    }
}
