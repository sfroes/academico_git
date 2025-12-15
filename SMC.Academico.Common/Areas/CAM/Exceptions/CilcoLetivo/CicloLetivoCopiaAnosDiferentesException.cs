using SMC.Academico.Common.Areas.CAM.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CAM.Exceptions
{
    public class CicloLetivoCopiaAnosDiferentesException : SMCApplicationException
    {
        public CicloLetivoCopiaAnosDiferentesException()
            : base(ExceptionsResource.ERR_ObrigatoriedadeSelecaoMesmoAno)
        {
        }
    }
}