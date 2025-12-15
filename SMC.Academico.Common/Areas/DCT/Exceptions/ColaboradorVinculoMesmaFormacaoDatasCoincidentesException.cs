using SMC.Academico.Common.Areas.DCT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.DCT.Exceptions
{
    public class ColaboradorVinculoMesmaFormacaoDatasCoincidentesException : SMCApplicationException
    {
        public ColaboradorVinculoMesmaFormacaoDatasCoincidentesException(string status)
            : base(string.Format(ExceptionsResource.ERR_ColaboradorVinculoMesmaFormacaoDatasCoincidentesException, status))
        {
        }
    }
}