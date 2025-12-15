using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class GrupoEscalonamentoAssociacaoProcessoEtapaExpiradosException : SMCApplicationException
    {
        public GrupoEscalonamentoAssociacaoProcessoEtapaExpiradosException()
            : base(ExceptionsResource.ERR_GrupoEscalonamentoAssociacaoProcessoEtapaExpiradosException)
        {
        }
    }
}