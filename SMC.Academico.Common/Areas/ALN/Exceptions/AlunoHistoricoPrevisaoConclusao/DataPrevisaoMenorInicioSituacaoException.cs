using SMC.Academico.Common.Areas.ALN.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class DataPrevisaoMenorInicioSituacaoException : SMCApplicationException
    {
        public DataPrevisaoMenorInicioSituacaoException()
            : base (ExceptionsResource.ERR_DataPrevisaoMenorInicioSituacaoException)
        { }
    }
}