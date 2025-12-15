using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class AtuacaoSemEmailException : SMCApplicationException
    {
        public AtuacaoSemEmailException()
            : base(Resources.ExceptionsResource.ERR_AtuacaoSemEmailException)
        { }
    }
}