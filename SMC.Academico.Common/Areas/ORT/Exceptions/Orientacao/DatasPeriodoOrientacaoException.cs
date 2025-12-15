using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class DatasPeriodoOrientacaoException : SMCApplicationException
    {
        public DatasPeriodoOrientacaoException(string status)
            : base(string.Format(ExceptionsResource.ERR_DatasPeriodoOrientacaoException, status))
        {
        }
    }
}