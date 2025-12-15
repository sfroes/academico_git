using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class OrigemParticipacaoOrientcaoInvalidaException : SMCApplicationException
    {
        public OrigemParticipacaoOrientcaoInvalidaException(TipoParticipacaoOrientacao tipoParticipacaoOrientacao, OrigemColaborador origemColaborador)
            : base(string.Format(ExceptionsResource.ERR_OrigemParticipacaoOrientcaoInvalidaException,
                tipoParticipacaoOrientacao.SMCGetDescription(),
                origemColaborador.SMCGetDescription()))
        {
        }
    }
}