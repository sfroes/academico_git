using SMC.Academico.Common.Areas.APR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.APR.Exceptions
{
    public class ListarException : SMCApplicationException
    {
        public ListarException()
            : base(ExceptionsResource.ERR_ListarException)
        {
        }
    }
}