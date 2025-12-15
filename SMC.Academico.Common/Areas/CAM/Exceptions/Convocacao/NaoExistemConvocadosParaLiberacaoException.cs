using SMC.Academico.Common.Areas.CAM.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CAM.Exceptions
{
    public class NaoExistemConvocadosParaLiberacaoException : SMCApplicationException
    {
        public NaoExistemConvocadosParaLiberacaoException()
            : base(ExceptionsResource.ERR_NaoExistemConvocadosParaLiberacaoException)
        {
        }
}
}