using SMC.Academico.Common.Areas.CAM.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CAM.Exceptions
{
    public class ReducaoVagaOfertaProcessoSeletivoInvalidaException : SMCApplicationException
    {
        public ReducaoVagaOfertaProcessoSeletivoInvalidaException(string qtdVagas, string oferta, string processoSeletivo)
            : base(string.Format(ExceptionsResource.ERR_ReducaoVagaOfertaProcessoSeletivoInvalidaException, qtdVagas, oferta, processoSeletivo))
        { }
    }
}
