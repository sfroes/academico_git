using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.ORT.Resources;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class PublicacaoBdpSemIdiomaPortuguesException : SMCApplicationException
    {
        public PublicacaoBdpSemIdiomaPortuguesException() 
            : base(ExceptionsResource.ERR_PublicacaoBdpSemIdiomaPortuguesException)
        { }
    }
}
