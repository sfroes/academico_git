using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class DispensaListaDuplicadoException : SMCApplicationException
    {
        public DispensaListaDuplicadoException()
            : base(ExceptionsResource.ERR_DispensaListaDuplicado)
        {
        }
    }
}