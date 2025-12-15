using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.SRC.Resources;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class AtributoAgendamentoJaAssociadoException : SMCApplicationException
    {
        public AtributoAgendamentoJaAssociadoException()
         : base(ExceptionsResource.ERR_AtributoAgendamentoJaAssociado)
        {
        }
    }
}
