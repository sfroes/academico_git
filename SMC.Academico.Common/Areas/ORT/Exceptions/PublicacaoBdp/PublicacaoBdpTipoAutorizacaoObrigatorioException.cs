using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;
using SMC.Framework.Util;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class PublicacaoBdpTipoAutorizacaoObrigatorioException : SMCApplicationException
    {
        public PublicacaoBdpTipoAutorizacaoObrigatorioException(TipoAutorizacao tipoAutorizacao) 
            : base(string.Format(ExceptionsResource.ERR_PublicacaoBdpTipoAutorizacaoObrigatorioException, SMCEnumHelper.GetDescription(tipoAutorizacao)))
        { }
    }
}
