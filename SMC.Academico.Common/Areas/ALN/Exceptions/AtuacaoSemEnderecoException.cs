using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class AtuacaoSemEnderecoException : SMCApplicationException
    {
        public AtuacaoSemEnderecoException()
            : base(Resources.ExceptionsResource.ERR_AtuacaoSemEnderecoException)
        { }
    }
}