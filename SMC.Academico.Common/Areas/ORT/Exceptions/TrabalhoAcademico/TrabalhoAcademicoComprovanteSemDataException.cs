using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class TrabalhoAcademicoComprovanteSemDataException : SMCApplicationException
    {
        public TrabalhoAcademicoComprovanteSemDataException()
            : base(ExceptionsResource.ERR_TrabalhoAcademicoComprovanteSemDataException)
        {
        }
    }
}