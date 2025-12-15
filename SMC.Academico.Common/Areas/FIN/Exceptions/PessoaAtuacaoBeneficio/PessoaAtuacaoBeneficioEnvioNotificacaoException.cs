using SMC.Academico.Common.Areas.FIN.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.FIN.Exceptions
{
    public class PessoaAtuacaoBeneficioEnvioNotificacaoException : SMCApplicationException
    {
        public PessoaAtuacaoBeneficioEnvioNotificacaoException(string token)
            : base(string.Format(ExceptionsResource.ERR_PessoaAtuacaoBeneficioEnvioNotificacaoException, token))
        { }
    }
}