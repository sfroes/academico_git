using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class TrabalhoAcademicoDataInvalidaException : SMCApplicationException
    {
        public TrabalhoAcademicoDataInvalidaException()
            : base(ExceptionsResource.ERR_TrabalhoAcademicoDataInvalidaException)
        {
        }
    }
}