using SMC.Academico.Common.Areas.CAM.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CAM.Exceptions.CilcoLetivo
{
    public class CicloLetivoInvalidoException : SMCApplicationException
    {
        public CicloLetivoInvalidoException()
            : base(ExceptionsResource.ERR_CicloLetivoInvalidoException)
        { }
    }
}