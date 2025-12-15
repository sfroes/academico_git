using SMC.Academico.Common.Areas.CSO.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CSO.Exceptions.TipoFormacaoEspecifica
{
    public class TipoFormacaoEspecificaTipoCursoDuplicadaException : SMCApplicationException
    {
        public TipoFormacaoEspecificaTipoCursoDuplicadaException()
            : base(ExceptionsResource.ERR_TipoFormacaoEspecificaTipoCursoDuplicadaException)
        {
        }
    }
}
