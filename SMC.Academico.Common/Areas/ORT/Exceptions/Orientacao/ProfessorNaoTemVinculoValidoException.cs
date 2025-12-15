using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORT.Exceptions.Orientacao
{
    public class ProfessorNaoTemVinculoValidoException : SMCApplicationException
    {
        public ProfessorNaoTemVinculoValidoException(string professores)
            : base(string.Format(ExceptionsResource.ERR_ProfessorNaoTemVinculoValidoException, professores))
        {
        }
    }
}