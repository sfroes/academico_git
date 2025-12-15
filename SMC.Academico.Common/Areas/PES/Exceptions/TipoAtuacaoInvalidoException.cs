using SMC.Academico.Common.Areas.PES.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.PES.Exceptions
{
    public class TipoAtuacaoInvalidoException : SMCApplicationException
    {
        public TipoAtuacaoInvalidoException()
            : base(ExceptionsResource.ERR_TipoAtuacaoInvalidoException)
        { }
    }
}