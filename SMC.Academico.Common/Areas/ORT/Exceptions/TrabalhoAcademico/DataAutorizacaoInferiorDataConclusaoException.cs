using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;
using System;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class DataAutorizacaoInferiorDataConclusaoException : SMCApplicationException
    {
        public DataAutorizacaoInferiorDataConclusaoException(DateTime data)
            : base(string.Format(ExceptionsResource.ERR_DataAutorizacaoInferiorDataConclusaoException, data.ToShortDateString()))
        {
        }
    }
}