using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class TrabalhoAcademicoSemDataException : SMCApplicationException
    {
        public TrabalhoAcademicoSemDataException()
            : base(ExceptionsResource.ERR_TrabalhoAcademicoSemDataException)
        {
        }
    }
}