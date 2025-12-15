using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework.Exceptions;
using SMC.Framework.Util;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class AssociacaoOrientadorIngressanteSemParametroException : SMCApplicationException
    {
        public AssociacaoOrientadorIngressanteSemParametroException(TipoParticipacaoOrientacao tipoParticipacaoOrientacao)
            : base(string.Format(Resources.ExceptionsResource.ERR_AssociacaoOrientadorIngressanteSemParametro, SMCEnumHelper.GetDescription(tipoParticipacaoOrientacao)))
        { }
    }
}