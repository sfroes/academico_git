using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.ORT.Resources;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class PublicacaoBdpLiberacaoConsultaSemArquivoException : SMCApplicationException
    {
        public PublicacaoBdpLiberacaoConsultaSemArquivoException() 
            : base(ExceptionsResource.ERR_PublicacaoBdpLiberacaoConsultaSemArquivoException)
        { }
    }
}
