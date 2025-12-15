using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class PublicacaoBdpIdiomaTrabalhoException : SMCApplicationException
    {
        public PublicacaoBdpIdiomaTrabalhoException()
            : base(ExceptionsResource.ERR_PublicacaoBdpIdiomaTrabalhoException)
        {
        }
    }
}