using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class TrabalhoAcademicoSemCicloLetivoException : SMCApplicationException
    {
        public TrabalhoAcademicoSemCicloLetivoException()
            : base(ExceptionsResource.ERR_TrabalhoAcademicoSemCicloLetivoException)
        {
        }
    }
}