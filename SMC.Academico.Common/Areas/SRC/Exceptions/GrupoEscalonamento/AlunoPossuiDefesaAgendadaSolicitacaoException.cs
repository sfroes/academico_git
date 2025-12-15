using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class AlunoPossuiDefesaAgendadaSolicitacaoException : SMCApplicationException
    {
        public AlunoPossuiDefesaAgendadaSolicitacaoException()
            : base(string.Format(ExceptionsResource.ERR_AlunoPossuiDefesaAgendadaSolicitacaoException))
        { }
    }
}