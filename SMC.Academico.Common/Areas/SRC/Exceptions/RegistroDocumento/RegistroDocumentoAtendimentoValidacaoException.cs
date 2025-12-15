using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class RegistroDocumentoAtendimentoValidacaoException : SMCApplicationException
    {
        public RegistroDocumentoAtendimentoValidacaoException()
            : base(ExceptionsResource.ERR_RegistroDocumentoAtendimentoValidacao)

        {
        }
    }
}