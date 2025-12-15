using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CNC.Exceptions
{
    public class CodigoVerificacaoNaoLocalizadoException : SMCApplicationException
    {
        public CodigoVerificacaoNaoLocalizadoException() : base(ExceptionsResource.ERR_CodigoVerificacaoNaoLocalizadoException)
        { }
    }
}
