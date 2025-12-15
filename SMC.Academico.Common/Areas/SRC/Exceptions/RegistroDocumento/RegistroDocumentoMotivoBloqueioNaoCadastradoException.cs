using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class RegistroDocumentoMotivoBloqueioNaoCadastradoException : SMCApplicationException
    {
        public RegistroDocumentoMotivoBloqueioNaoCadastradoException()
            : base(ExceptionsResource.ERR_RegistroDocumentoMotivoBloqueioNaoCadastrado)

        {
        }
    }
}