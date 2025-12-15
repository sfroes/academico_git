using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class TrabalhoAcademicoComAvaliacaoException : SMCApplicationException
    {
        public TrabalhoAcademicoComAvaliacaoException()
            : base(ExceptionsResource.ERR_TrabalhoAcademicoComAvaliacaoException)
        {
        }
    }
}