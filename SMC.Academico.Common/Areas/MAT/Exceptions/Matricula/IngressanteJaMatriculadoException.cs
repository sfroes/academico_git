using SMC.Academico.Common.Areas.MAT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.MAT.Exceptions
{
    public class IngressanteJaMatriculadoException : SMCApplicationException
    {
        public IngressanteJaMatriculadoException()
            : base(ExceptionsResource.ERR_IngressanteJaMatriculadoException)
        {
        }
    }
}