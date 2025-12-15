using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class AtuacaoSemTelefoneException : SMCApplicationException
    {
        public AtuacaoSemTelefoneException()
            : base(Resources.ExceptionsResource.ERR_AtuacaoSemTelefoneException)
        { }
    }
}