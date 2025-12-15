using SMC.Academico.Common.Areas.CAM.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CAM.Exceptions
{
    public class CampanhaTipoProcessoNaoVilculadoNivelEnsinoVinculoFormaIngressoException : SMCApplicationException
    {
        public CampanhaTipoProcessoNaoVilculadoNivelEnsinoVinculoFormaIngressoException(string processoSeletivo)
            : base(string.Format(ExceptionsResource.ERR_CampanhaTipoProcessoNaoVilculadoNivelEnsinoVinculoFormaIngressoException, processoSeletivo))
        { }
    }
}