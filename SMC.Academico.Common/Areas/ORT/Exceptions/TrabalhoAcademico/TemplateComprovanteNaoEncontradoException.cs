using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.ORT.Resources;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class TemplateComprovanteNaoEncontradoException : SMCApplicationException
    {
        public TemplateComprovanteNaoEncontradoException()
        : base(ExceptionsResource.ERR_Template_comprovante_nao_encontrado)
        {
        }
    }
}
