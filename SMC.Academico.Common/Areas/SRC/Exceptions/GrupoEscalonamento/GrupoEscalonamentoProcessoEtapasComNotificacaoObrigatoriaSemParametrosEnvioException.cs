using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class GrupoEscalonamentoProcessoEtapasComNotificacaoObrigatoriaSemParametrosEnvioException : SMCApplicationException
    {
        public GrupoEscalonamentoProcessoEtapasComNotificacaoObrigatoriaSemParametrosEnvioException(string etapasENotificacoes)
            : base(string.Format(ExceptionsResource.ERR_GrupoEscalonamentoProcessoEtapasComNotificacaoObrigatoriaSemParametrosEnvioException, etapasENotificacoes))
        {
        }
    }
}