using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.PES.Resources;

namespace SMC.Academico.Common.Areas.PES.Exceptions
{
    public class EnvioNotificacaoSelecaoPessoaAtuacaoException : SMCApplicationException
    {
        public EnvioNotificacaoSelecaoPessoaAtuacaoException(int quantidade)
            : base(string.Format(ExceptionsResource.ERR_EnvioNotificacaoSelecaoPessoaAtuacaoException, quantidade))
        {     
        }
    }
}
