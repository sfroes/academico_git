using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class GrupoEscalonamentoAssociacaoCodigoAdesaoGrupoDiferenteException : SMCApplicationException
    {
        public GrupoEscalonamentoAssociacaoCodigoAdesaoGrupoDiferenteException()
            : base(ExceptionsResource.ERR_GrupoEscalonamentoAssociacaoCodigoAdesaoGrupoDiferente)
        {
        }
    }
}