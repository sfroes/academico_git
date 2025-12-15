using SMC.Academico.Common.Areas.DCT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.DCT.Exceptions
{
    public class ColaboradorVinculoEntidadeTipoDataCoincidenteException : SMCApplicationException
    {
        public ColaboradorVinculoEntidadeTipoDataCoincidenteException(string status)
            : base(string.Format(ExceptionsResource.ERR_ColaboradorVinculoEntidadeTipoDataCoincidenteException, status))
        {
        }
    }
}