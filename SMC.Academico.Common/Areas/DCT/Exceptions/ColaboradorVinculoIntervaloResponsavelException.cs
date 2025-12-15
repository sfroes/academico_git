using SMC.Academico.Common.Areas.DCT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.DCT.Exceptions
{
    public class ColaboradorVinculoIntervaloResponsavelException : SMCApplicationException
    {
        public ColaboradorVinculoIntervaloResponsavelException()
            : base(ExceptionsResource.ERR_ColaboradorVinculoIntervaloResponsavelException)
        {
        }
    }
}