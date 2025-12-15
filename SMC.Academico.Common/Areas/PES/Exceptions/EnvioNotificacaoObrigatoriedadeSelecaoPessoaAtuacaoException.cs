using SMC.Academico.Common.Areas.PES.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.PES.Exceptions
{
    public class EnvioNotificacaoObrigatoriedadeSelecaoPessoaAtuacaoException : SMCApplicationException
    {
        public EnvioNotificacaoObrigatoriedadeSelecaoPessoaAtuacaoException(string pessoaAtuacao)
            : base(string.Format(ExceptionsResource.ERR_ObrigatoriedadeSelecaoPessoaAtuacao, pessoaAtuacao))
        {
        }
    }
}