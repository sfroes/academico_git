using SMC.Academico.Common.Areas.DCT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.DCT.Exceptions
{
    public class ColaboradorVinculoResponsavelObrigatorioException : SMCApplicationException
    {
        public ColaboradorVinculoResponsavelObrigatorioException()
            : base(ExceptionsResource.ERR_ColaboradorVinculoResponsavelObrigatorioException)
        {
        }
    }
}