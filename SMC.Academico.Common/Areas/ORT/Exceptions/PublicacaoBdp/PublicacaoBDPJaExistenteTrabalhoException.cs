using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class PublicacaoBDPJaExistenteTrabalhoException : SMCApplicationException
    {
        public PublicacaoBDPJaExistenteTrabalhoException()
            : base(ExceptionsResource.ERR_PublicacaoBDPJaExistenteTrabalhoException)
        {
        }
    }
}