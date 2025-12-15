using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.PES.Resources;
using SMC.Framework.Exceptions;
using SMC.Framework.Util;

namespace SMC.Academico.Common.Areas.PES.Exceptions
{
    public class InstituicaoTipoAtuacaoNaoConfiguradaException : SMCApplicationException
    {
        public InstituicaoTipoAtuacaoNaoConfiguradaException(TipoAtuacao TipoAtuacao)
            : base(string.Format(ExceptionsResource.ERR_InstituicaoTipoAtuacaoNaoConfiguradaException, SMCEnumHelper.GetDescription(TipoAtuacao).ToLower()))
        {
        }
    }
}