using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class GrupoEscalonamentoAssociacaoSemVagasException : SMCApplicationException
    {
        public GrupoEscalonamentoAssociacaoSemVagasException(string listaTurmas)
            : base(string.Format(ExceptionsResource.ERR_GrupoEscalonamentoAssociacaoSemVagasException, listaTurmas))
        {
        }
    }
}