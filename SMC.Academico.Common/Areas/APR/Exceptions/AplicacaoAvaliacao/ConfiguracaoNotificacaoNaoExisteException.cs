using SMC.Academico.Common.Areas.APR.Resources;
using SMC.Framework.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Common.Areas.APR.Exceptions.AplicacaoAvaliacao
{
    public class ConfiguracaoNotificacaoNaoExisteException : SMCApplicationException
    {
        public ConfiguracaoNotificacaoNaoExisteException()
            : base(ExceptionsResource.ERR_ConfiguracaoNotificacaoNaoExisteException)
        {
        }
    }
}
