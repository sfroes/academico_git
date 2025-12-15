using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class AlteracaoDadosNaoPermitidaException : SMCApplicationException
    {
        public AlteracaoDadosNaoPermitidaException()
            : base(Resources.ExceptionsResource.ERR_AlteracaoDadosNaoPermitida)
        { }
    }
}