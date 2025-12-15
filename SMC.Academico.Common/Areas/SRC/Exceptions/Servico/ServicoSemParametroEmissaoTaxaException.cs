using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ServicoSemParametroEmissaoTaxaException : SMCApplicationException
    {
        public ServicoSemParametroEmissaoTaxaException() : 
            base(ExceptionsResource.ERR_ServicoSemParametroEmissaoTaxaException)
        {
        }
    }
}