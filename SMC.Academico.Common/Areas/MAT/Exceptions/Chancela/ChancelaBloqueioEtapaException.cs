using SMC.Academico.Common.Areas.MAT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.MAT.Exceptions
{
    public class ChancelaBloqueioEtapaException : SMCApplicationException
    {
        public ChancelaBloqueioEtapaException()
            : base(ExceptionsResource.ERR_ChancelaBloqueioEtapa)
        { }
    }
}