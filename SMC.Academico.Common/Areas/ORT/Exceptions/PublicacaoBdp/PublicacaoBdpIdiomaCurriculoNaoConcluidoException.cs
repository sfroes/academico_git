using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class PublicacaoBdpIdiomaCurriculoNaoConcluidoException : SMCApplicationException
    {
        public PublicacaoBdpIdiomaCurriculoNaoConcluidoException() 
            : base(ExceptionsResource.ERR_PublicacaoBdpIdiomaCurriculoNaoConcluidoException)
        { }
    }
}
