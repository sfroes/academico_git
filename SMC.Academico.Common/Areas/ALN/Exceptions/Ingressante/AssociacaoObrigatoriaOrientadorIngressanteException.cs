using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework.Exceptions;
using SMC.Framework.Util;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class AssociacaoObrigatoriaOrientadorIngressanteException : SMCApplicationException
    {
        public AssociacaoObrigatoriaOrientadorIngressanteException(TipoParticipacaoOrientacao tipoParticipacaoOrientacao)
            : base(string.Format(Resources.ExceptionsResource.ERR_AssociacaoObrigatoriaOrientadorIngressante, SMCEnumHelper.GetDescription(tipoParticipacaoOrientacao)))
        { }
    }
}