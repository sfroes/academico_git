using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class TrabalhoAcademicoNaoExisteException : SMCApplicationException
    {
        public TrabalhoAcademicoNaoExisteException()
            : base(ExceptionsResource.ERR_TrabalhoAcademicoNaoExisteException)
        {
        }
    }
}