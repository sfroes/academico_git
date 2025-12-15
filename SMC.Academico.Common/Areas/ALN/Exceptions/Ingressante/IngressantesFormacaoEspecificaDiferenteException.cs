using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class IngressantesFormacaoEspecificaDiferenteException : SMCApplicationException
    {
        public IngressantesFormacaoEspecificaDiferenteException()
            : base(Resources.ExceptionsResource.ERR_IngressantesFormacaoEspecificaDiferente)
        { }
    }
}