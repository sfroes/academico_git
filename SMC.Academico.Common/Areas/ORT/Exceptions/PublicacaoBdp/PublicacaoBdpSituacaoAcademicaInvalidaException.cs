using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.ORT.Resources;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class PublicacaoBdpSituacaoAcademicaInvalidaException : SMCApplicationException
    {
        public PublicacaoBdpSituacaoAcademicaInvalidaException() 
            : base(ExceptionsResource.ERR_PublicacaoBdpSituacaoAcademicaInvalidaException)
        { }
    }
}
