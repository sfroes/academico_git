using SMC.Academico.Common.Areas.FIN.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.FIN.Exceptions
{
    public class TemplateTermoConcessaoBolsaNaoEncontradoException : SMCApplicationException
    {
        public TemplateTermoConcessaoBolsaNaoEncontradoException() 
            : base(ExceptionsResource.ERR_TemplateTermoConcessaoBolsaNaoEncontradoException)
        { }
    }
}
