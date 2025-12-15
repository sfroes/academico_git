using SMC.Academico.Common.Areas.CAM.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CAM.Exceptions
{
    public class ImpossivelAlterarAbrangenciaException : SMCApplicationException
    {
        public ImpossivelAlterarAbrangenciaException()
            : base(ExceptionsResource.ERR_ImpossivelAlterarAbrangenciaException)
        {
        }
    }
}