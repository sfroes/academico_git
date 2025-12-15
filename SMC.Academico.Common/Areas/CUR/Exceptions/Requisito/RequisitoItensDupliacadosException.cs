using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class RequisitoItensDupliacadosException : SMCApplicationException
    {
        public RequisitoItensDupliacadosException()
            : base(ExceptionsResource.ERR_RequisitoItensDupliacadosException)
        {
        }
    }
}