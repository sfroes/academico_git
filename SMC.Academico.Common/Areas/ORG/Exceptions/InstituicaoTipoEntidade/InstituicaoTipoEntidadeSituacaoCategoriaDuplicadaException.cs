using SMC.Framework.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMC.Academico.Common.Areas.ORG.Resources;

namespace SMC.Academico.Common.ORG.Exceptions
{
    public class InstituicaoTipoEntidadeSituacaoCategoriaDuplicadaException : SMCApplicationException
    {
        public InstituicaoTipoEntidadeSituacaoCategoriaDuplicadaException()
            : base(ExceptionsResource.ERR_InstituicaoTipoEntidadeSituacaoCategoriaDuplicadaException)
        {
        }
    }
}