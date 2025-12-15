using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class TemplatePublicacaoBdpNaoEncontradoException : SMCApplicationException
    {
        public TemplatePublicacaoBdpNaoEncontradoException() 
            : base(ExceptionsResource.ERR_TemplatePublicacaoBdpNaoEncontradoException)
        { }
    }
}
