using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;
using System;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class DataAutorizacaoMaiorDataFimException : SMCApplicationException
    {
        public DataAutorizacaoMaiorDataFimException(DateTime dataFim)
            : base(string.Format(ExceptionsResource.ERR_DataAutorizacaoMaiorDataFimException, dataFim.ToShortDateString()))
        {
        }
    }
}