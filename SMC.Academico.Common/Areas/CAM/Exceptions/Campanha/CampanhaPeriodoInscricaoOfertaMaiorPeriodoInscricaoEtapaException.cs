using SMC.Academico.Common.Areas.CAM.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CAM.Exceptions
{
    public class CampanhaPeriodoInscricaoOfertaMaiorPeriodoInscricaoEtapaException : SMCApplicationException
    {
        public CampanhaPeriodoInscricaoOfertaMaiorPeriodoInscricaoEtapaException()
            : base(ExceptionsResource.ERR_CampanhaPeriodoInscricaoOfertaMaiorPeriodoInscricaoEtapaException)
        { }
    }
}