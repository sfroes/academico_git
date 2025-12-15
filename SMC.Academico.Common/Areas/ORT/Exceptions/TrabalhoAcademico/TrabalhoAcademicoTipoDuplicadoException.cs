using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class TrabalhoAcademicoTipoDuplicadoException : SMCApplicationException
    {
        public TrabalhoAcademicoTipoDuplicadoException()
            : base(ExceptionsResource.ERR_TrabalhoAcademicoTipoDuplicadoException)
        {
        }
    }
}