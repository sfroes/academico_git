using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class RegistroDocumentoAtendimentoValidacaoDiplomaException : SMCApplicationException
    {
        public RegistroDocumentoAtendimentoValidacaoDiplomaException()
            : base(ExceptionsResource.ERR_RegistroDocumentoAtendimentoValidacaoDiploma)

        {
        }
    }
}