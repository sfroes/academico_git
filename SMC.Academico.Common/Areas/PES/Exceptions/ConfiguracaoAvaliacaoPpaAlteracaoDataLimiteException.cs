using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.PES.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Common.Areas.PES.Exceptions
{
    public class ConfiguracaoAvaliacaoPpaAlteracaoDataLimiteException : SMCApplicationException
    {
        public ConfiguracaoAvaliacaoPpaAlteracaoDataLimiteException()
          : base(ExceptionsResource.ERR_ConfiguracaoAvaliacaoPpaAlteracaoDataLimiteException)
        { }
    }
}
