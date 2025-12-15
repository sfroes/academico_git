using SMC.Framework.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMC.Academico.Common.Areas.ALN.Resources;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class InstituicaoNivelTipoOrientacaoTipoTermoException : SMCApplicationException
    {
        public InstituicaoNivelTipoOrientacaoTipoTermoException(string status)
            : base(string.Format(ExceptionsResource.ERR_InstituicaoNivelTipoOrientacaoTipoTermoException, status))
        {

        }
    }
}
