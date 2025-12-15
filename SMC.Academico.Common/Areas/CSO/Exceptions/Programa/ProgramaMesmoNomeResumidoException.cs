using SMC.Academico.Common.Areas.CSO.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CSO.Exceptions
{
    public class ProgramaMesmoNomeResumidoException : SMCApplicationException
    {
        public ProgramaMesmoNomeResumidoException()
            : base(ExceptionsResource.ERR_ProgramaMesmoNomeResumidoException)
        {
        }
    }
}