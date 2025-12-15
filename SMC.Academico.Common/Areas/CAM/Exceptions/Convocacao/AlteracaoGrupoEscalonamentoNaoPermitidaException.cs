using SMC.Academico.Common.Areas.CAM.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CAM.Exceptions
{
    public class AlteracaoGrupoEscalonamentoNaoPermitidaException : SMCApplicationException
    {
        public AlteracaoGrupoEscalonamentoNaoPermitidaException(string numeroChamada)
        : base(string.Format(ExceptionsResource.ERR_Alterar_Grupo_escalonamento_Exception, numeroChamada))
        {}
    }
}
