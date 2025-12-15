using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class RequisitoSemDivisaoException : SMCApplicationException
    {
        public RequisitoSemDivisaoException()
            : base(ExceptionsResource.ERR_RequisitoSemDivisaoException)
        {
        }
    }
}