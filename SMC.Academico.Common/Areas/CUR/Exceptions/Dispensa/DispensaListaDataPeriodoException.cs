using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class DispensaListaDataPeriodoException : SMCApplicationException
    {
        public DispensaListaDataPeriodoException()
            : base(ExceptionsResource.ERR_DispensaListaDataPeriodo)
        {
        }
    }
}