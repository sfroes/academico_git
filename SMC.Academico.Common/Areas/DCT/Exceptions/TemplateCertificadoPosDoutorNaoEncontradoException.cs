using SMC.Academico.Common.Areas.DCT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.DCT.Exceptions
{
    public class TemplateCertificadoPosDoutorNaoEncontradoException : SMCApplicationException
    {
        public TemplateCertificadoPosDoutorNaoEncontradoException() 
            : base(ExceptionsResource.ERR_TemplateCertificadoPosDoutorNaoEncontradoException)
        { }
    }
}
