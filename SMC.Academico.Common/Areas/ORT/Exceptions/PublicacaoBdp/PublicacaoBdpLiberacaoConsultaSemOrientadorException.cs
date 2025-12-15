using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.ORT.Resources;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class PublicacaoBdpLiberacaoConsultaSemOrientadorException : SMCApplicationException
    {
        public PublicacaoBdpLiberacaoConsultaSemOrientadorException() 
            : base(ExceptionsResource.ERR_PublicacaoBdpLiberacaoConsultaSemOrientadorException)
        { }
    }
}
