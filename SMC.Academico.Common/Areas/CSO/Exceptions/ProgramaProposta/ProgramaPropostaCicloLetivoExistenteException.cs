using SMC.Academico.Common.Areas.CSO.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CSO.Exceptions
{
    public class ProgramaPropostaCicloLetivoExistenteException : SMCApplicationException
    {
        public ProgramaPropostaCicloLetivoExistenteException()
            : base(ExceptionsResource.ERR_ProgramaPropostaCicloLetivoExistenteException)
        {
        }
    }
}