using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class TermoIntercambioPossuiPessoaAtuacaoException : SMCApplicationException
    {
        public TermoIntercambioPossuiPessoaAtuacaoException()
            : base(Resources.ExceptionsResource.ERR_TermoIntercambioPossuiPessoaAtuacaoException)
        { }
    }
}

