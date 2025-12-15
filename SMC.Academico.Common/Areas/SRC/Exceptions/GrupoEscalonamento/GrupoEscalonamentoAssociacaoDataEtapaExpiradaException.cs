using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class GrupoEscalonamentoAssociacaoDataEtapaExpiradaException : SMCApplicationException
    {
        public GrupoEscalonamentoAssociacaoDataEtapaExpiradaException(string dscEtapa)
            : base(string.Format(ExceptionsResource.ERR_GrupoEscalonamentoAssociacaoDataEtapaExpiradaException, dscEtapa))
        {
        }
    }
}