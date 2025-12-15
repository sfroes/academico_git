using SMC.Academico.Common.Areas.ALN.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class DataPrevisaoMenorAutorizacaoException : SMCApplicationException
    {
        public DataPrevisaoMenorAutorizacaoException()
            : base (ExceptionsResource.ERR_DataPrevisaoMenorAutorizacaoException)
        { }
    }
}