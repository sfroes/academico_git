using SMC.Academico.Common.Areas.CAM.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CAM.Exceptions
{
    public class ReducaoVagaOfertaConvocacaoInvalidaException : SMCApplicationException
    {
        public ReducaoVagaOfertaConvocacaoInvalidaException(string qtdVagas, string oferta, string convocacao)
            : base(string.Format(ExceptionsResource.ERR_ReducaoVagaOfertaConvocacaoInvalidaException, qtdVagas, oferta, convocacao))
        { }
    }
}
