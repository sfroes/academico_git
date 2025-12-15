using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class GrupoEscalonamentoAssociacaoGrupoNovoIgualGrupoAtualException : SMCApplicationException
    {
        public GrupoEscalonamentoAssociacaoGrupoNovoIgualGrupoAtualException()
            : base(ExceptionsResource.ERR_GrupoEscalonamentoAssociacaoGrupoNovoIgualGrupoAtualException)
        {
        }
    }
}