using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class DispensaListaVaziaException : SMCApplicationException
    {
        public DispensaListaVaziaException()
            : base(ExceptionsResource.ERR_DispensaListaVazia)
        {
        }
    }
}