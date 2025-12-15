using SMC.Academico.Common.Areas.DCT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.DCT.Exceptions
{
    public class ColaboradorVinculoDatasPeriodoException : SMCApplicationException
    {
        public ColaboradorVinculoDatasPeriodoException()
            : base(ExceptionsResource.ERR_ColaboradorVinculoDatasPeriodoException)
        {
        }
    }
}