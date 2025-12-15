using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class RequisitoQuantidadeCreditoInvalidaException : SMCApplicationException
    {
        public RequisitoQuantidadeCreditoInvalidaException()
            : base(ExceptionsResource.ERR_RequisitoQuantidadeCreditoInvalidaException)
        {
        }
    }
}