using SMC.Academico.Common.Areas.PES.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.PES.Exceptions
{
    public class FuncionarioVinculoConcidentesException : SMCApplicationException
    {
        public FuncionarioVinculoConcidentesException()
            : base(ExceptionsResource.ERR_FuncionarioVinculoConcidentesException)
        {
        }
    }
}