using SMC.Academico.Common.Areas.DCT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.DCT.Exceptions
{
    public class NaoExisteProfessorAssociadoException : SMCApplicationException
    {
        public NaoExisteProfessorAssociadoException()
            : base(ExceptionsResource.ERR_NaoExisteProfessorAssociadoException)
        {
        }
    }
}