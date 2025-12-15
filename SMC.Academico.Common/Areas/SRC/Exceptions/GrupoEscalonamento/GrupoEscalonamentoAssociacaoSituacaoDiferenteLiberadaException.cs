using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class GrupoEscalonamentoAssociacaoSituacaoDiferenteLiberadaException : SMCApplicationException
    {
        public GrupoEscalonamentoAssociacaoSituacaoDiferenteLiberadaException(string dscEtapa)
            : base(string.Format(ExceptionsResource.ERR_GrupoEscalonamentoAssociacaoSituacaoDiferenteLiberadaException, dscEtapa))
        {
        }
    }
}