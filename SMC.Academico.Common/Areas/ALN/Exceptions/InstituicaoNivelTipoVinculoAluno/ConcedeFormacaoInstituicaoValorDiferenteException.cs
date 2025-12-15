using SMC.Academico.Common.Areas.ALN.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class ConcedeFormacaoInstituicaoValorDiferenteException : SMCApplicationException
    {
        public ConcedeFormacaoInstituicaoValorDiferenteException(string registros)
        : base(string.Format(ExceptionsResource.ERR_ConcedeFormacaoInstituicaoValorDiferenteException, registros))
        {
        }
    }
}