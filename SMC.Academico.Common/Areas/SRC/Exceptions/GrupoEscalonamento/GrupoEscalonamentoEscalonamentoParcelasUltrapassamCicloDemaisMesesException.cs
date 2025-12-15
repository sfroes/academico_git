using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class GrupoEscalonamentoEscalonamentoParcelasUltrapassamCicloDemaisMesesException : SMCApplicationException
    {
        public GrupoEscalonamentoEscalonamentoParcelasUltrapassamCicloDemaisMesesException(int qtdeMesesCicloMenosOrdemMesCicloDataFimEscalonamento)
            : base(string.Format(ExceptionsResource.ERR_GrupoEscalonamentoEscalonamentoParcelasUltrapassamCicloDemaisMeses, qtdeMesesCicloMenosOrdemMesCicloDataFimEscalonamento))
        {
        }
    }
}