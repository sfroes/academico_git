using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class DataAutorizacaoInferiorDataDefesaException : SMCApplicationException
    {
        public DataAutorizacaoInferiorDataDefesaException()
            : base(ExceptionsResource.ERR_DataAutorizacaoInferiorDataDefesaException)
        {
        }
    }
}