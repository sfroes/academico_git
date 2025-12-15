using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class PublicacaoBdpIdiomaSemPalavraChaveException : SMCApplicationException
    {
        public PublicacaoBdpIdiomaSemPalavraChaveException() 
            : base(ExceptionsResource.ERR_PublicacaoBdpIdiomaSemPalavraChaveException)
        { }
    }
}
