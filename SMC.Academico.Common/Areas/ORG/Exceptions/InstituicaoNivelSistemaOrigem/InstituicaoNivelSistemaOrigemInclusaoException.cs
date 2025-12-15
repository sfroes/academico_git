using SMC.Academico.Common.Areas.ORG.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORG.Exceptions.InstituicaoNivelSistemaOrigem
{
    public class InstituicaoNivelSistemaOrigemInclusaoException : SMCApplicationException
    {
        public InstituicaoNivelSistemaOrigemInclusaoException() : base(ExceptionsResource.ERR_InstituicaoNivelSistemaOrigemInclusao)
        {}
    }
}