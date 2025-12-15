using SMC.Academico.Common.Areas.MAT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.MAT.Exceptions
{
    public class AlunoPossuiDefesaAgendadaException : SMCApplicationException
    {
        public AlunoPossuiDefesaAgendadaException(string nomeAluno)
            : base(string.Format(ExceptionsResource.ERR_AlunoPossuiDefesaAgendadaException, nomeAluno))
        { }
    }
}