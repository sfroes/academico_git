using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class TrabalhoAcademicoQuantidadeInvalidaAlunosException : SMCApplicationException
    {
        public TrabalhoAcademicoQuantidadeInvalidaAlunosException()
            : base(ExceptionsResource.ERR_TrabalhoAcademicoQuantidadeInvalidaAlunosException)
        {
        }
    }
}