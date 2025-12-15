using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class DispensaListaDataMatrizExcecaoException : SMCApplicationException
    {
        public DispensaListaDataMatrizExcecaoException()
            : base(ExceptionsResource.ERR_DispensaListaDataMatrizExcecao)
        {
        }
    }
}