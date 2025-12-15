using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class RequisitoSemItensException : SMCApplicationException
    {
        public RequisitoSemItensException()
            : base(ExceptionsResource.ERR_RequisitoSemItensException)
        {
        }
    }
}