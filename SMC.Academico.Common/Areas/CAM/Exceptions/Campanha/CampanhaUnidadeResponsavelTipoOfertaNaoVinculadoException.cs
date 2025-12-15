using SMC.Academico.Common.Areas.CAM.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CAM.Exceptions
{
    public class CampanhaUnidadeResponsavelTipoOfertaNaoVinculadoException : SMCApplicationException
    {
        public CampanhaUnidadeResponsavelTipoOfertaNaoVinculadoException(string tipoOferta, string unidadeResponsavel)
            : base(string.Format(ExceptionsResource.ERR_CampanhaUnidadeResponsavelTipoOfertaNaoVinculadoException, tipoOferta, unidadeResponsavel))
        { }
    }
}